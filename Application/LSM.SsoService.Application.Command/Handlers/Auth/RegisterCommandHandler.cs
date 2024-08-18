using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Requests.Auth;
using LSM.SsoService.Application.Common.Result;
using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects;
using LSM.SsoService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LSM.SsoService.Application.Command.Handlers.Auth;

public sealed class RegisterCommandHandler(
    IDataContext dataContext
) : ICommandHandler<RegisterCommand>
{
    private const string EmailAlreadyTaken = "Email {0} уже занят.";

    public async Task<EmptyResult> HandleAsync(
        RegisterCommand command,
        CancellationToken ct = default)
    {
        var emailParseResult = Email.Parse(command.Email);
        if (emailParseResult.IsFailure)
            return EmptyResult.Failure(
                Error.Create(ErrorGroup.Validation, emailParseResult.Error)
            );

        var email = emailParseResult.Value;
        if (await UserWithEmailExistsAsync(email, ct))
            return EmptyResult.Failure(
                Error.Create(ErrorGroup.AlreadyExists, string.Format(EmailAlreadyTaken, email))
            );

        var user = User.Create(
            username: command.Username,
            password: command.Password,
            name: command.Name,
            surname: command.Surname,
            birthDate: command.BirthDate,
            patronymic: command.Patronymic ?? Maybe<string>.None,
            email: email
        );
        await AddUserAsync(user, ct);

        return EmptyResult.Success();
    }

    private Task<bool> UserWithEmailExistsAsync(Email email, CancellationToken ct = default)
        => dataContext.Users
            .AnyAsync(x => x.Email == email, ct);

    private async Task AddUserAsync(User user, CancellationToken ct = default)
    {
        dataContext.Users.Add(user);
        await dataContext.SaveChangesAsync(ct);
    }
}
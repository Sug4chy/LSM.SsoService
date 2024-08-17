using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Requests.Auth;
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

    public async Task<Result> HandleAsync(RegisterCommand command, CancellationToken ct = default)
    {
        Email? email = null;
        if (command.Email is not null)
        {
            var emailParseResult = Email.Parse(command.Email);
            if (emailParseResult.IsFailure)
                return emailParseResult.ConvertFailure<Result>();

            email = emailParseResult.Value;
            if (await UserWithEmailExists(email, ct))
                return Result.Failure(string.Format(EmailAlreadyTaken, email));
        }

        var user = User.Create(
            username: command.Username,
            password: command.Password,
            name: command.Name,
            surname: command.Surname,
            birthDate: command.BirthDate,
            patronymic: command.Patronymic ?? Maybe<string>.None, 
            email: email ?? Maybe<Email>.None
        );
        dataContext.Users.Add(user);
        await dataContext.SaveChangesAsync(ct);

        return Result.Success();
    }

    private Task<bool> UserWithEmailExists(Email email, CancellationToken ct = default)
        => dataContext.Users
            .AnyAsync(x => x.Email == email, ct);
}
using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Requests.Auth;
using LSM.SsoService.Application.Command.Responses.Auth;
using LSM.SsoService.Application.Command.Validation;
using LSM.SsoService.Application.Common.Results;
using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects;
using LSM.SsoService.Infrastructure.Jwt.Services;
using LSM.SsoService.Infrastructure.Persistence.Context;
using LSM.SsoService.Infrastructure.Persistence.Extensions;

namespace LSM.SsoService.Application.Command.Handlers.Auth;

public sealed class LoginCommandHandler(
    IDataContext dataContext,
    IJwtGenerator jwtGenerator
) : ICommandHandler<LoginCommand, JwtResponse>
{
    private const string InvalidUsernameOrPassword = "Имя пользователя или пароль введены неверно.";

    public async Task<Result<JwtResponse, Error>> HandleAsync(LoginCommand command, CancellationToken ct = default)
    {
        var (_, isFailure, email) = Email.Parse(command.Email);
        if (isFailure)
            return Result.Failure<JwtResponse, Error>(
                Error.Create(ErrorGroup.Validation, ValidationMessages.InvalidEmail)
            );

        var maybeUser = await GetUserByEmailAndPasswordAsync(email, command.Password, ct);
        if (maybeUser.HasNoValue)
            return Result.Failure<JwtResponse, Error>(
                Error.Create(ErrorGroup.NotFound, InvalidUsernameOrPassword)
            );

        var user = maybeUser.Value;
        string token = jwtGenerator.GenerateForUser(user);
        
        return Result.Success<JwtResponse, Error>(
            new JwtResponse(token)
        );
    }

    private Task<Maybe<User>> GetUserByEmailAndPasswordAsync(
        Email email,
        string password,
        CancellationToken ct = default)
        => dataContext.Users
            .TryFirstAsync(x => x.Email == email
                                && x.Password == password, ct);
}
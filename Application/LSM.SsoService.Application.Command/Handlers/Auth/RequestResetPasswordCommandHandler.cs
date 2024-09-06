using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Requests.Auth;
using LSM.SsoService.Application.Common.Results;
using LSM.SsoService.Domain.Entities;
using LSM.SsoService.Domain.ValueObjects;
using LSM.SsoService.Domain.ValueObjects.Tokens;
using LSM.SsoService.Infrastructure.Messaging.Services;
using LSM.SsoService.Infrastructure.Persistence.Context;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using LSM.SsoService.Infrastructure.Tokens.Services;

namespace LSM.SsoService.Application.Command.Handlers.Auth;

public sealed class RequestResetPasswordCommandHandler(
    IDataContext dataContext,
    ITokenSource<ResetPasswordToken> tokenSource,
    IMessagingService messaging
) : ICommandHandler<RequestResetPasswordCommand>
{
    public async Task<EmptyResult> HandleAsync(RequestResetPasswordCommand command, CancellationToken ct = default)
    {
        var (_, isFailure, email) = Email.Parse(command.Email);
        if (isFailure)
            return EmptyResult.Failure(
                Error.Create(ErrorGroup.Validation, "Invalid email")
            );

        var maybeUser = await GetUserByEmailAsync(email, ct);
        if (maybeUser.HasNoValue)
            return EmptyResult.Failure(
                Error.Create(ErrorGroup.NotFound, "User not found")
            );

        var user = maybeUser.Value;
        var token = tokenSource.ResetPasswordTokenFor(user);
        await dataContext.SaveChangesAsync(ct);

        await messaging.SendResetPasswordEmailAsync(email, token.Token, ct);

        return EmptyResult.Success();
    }

    private Task<Maybe<User>> GetUserByEmailAsync(Email email, CancellationToken ct = default)
        => dataContext.Users
            .TryFirstAsync(x => x.Email == email, ct);
}
using CSharpFunctionalExtensions;
using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Requests.Auth;

namespace LSM.SsoService.Application.Command.Handlers.Auth;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    public Task<Result> HandleAsync(RegisterCommand command, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
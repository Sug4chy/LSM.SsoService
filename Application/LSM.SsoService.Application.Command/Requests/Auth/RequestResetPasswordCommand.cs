using LSM.SsoService.Application.Command.Interfaces;

namespace LSM.SsoService.Application.Command.Requests.Auth;

public sealed record RequestResetPasswordCommand(
    string Email
) : ICommand;
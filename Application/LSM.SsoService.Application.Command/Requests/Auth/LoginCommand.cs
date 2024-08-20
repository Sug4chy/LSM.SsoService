using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Responses.Auth;
using LSM.SsoService.Application.Command.Validation.Auth;
using LSM.SsoService.Application.Common.Validation.Attributes;

namespace LSM.SsoService.Application.Command.Requests.Auth;

[Validator<LoginCommandValidator>]
public sealed record LoginCommand(
    string Email,
    string Password
) : ICommand<JwtResponse>;
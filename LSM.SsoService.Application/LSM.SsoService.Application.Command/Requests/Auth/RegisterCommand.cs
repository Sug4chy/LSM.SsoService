using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Validators.Auth;
using LSM.SsoService.Application.Common.Validation.Attributes;

namespace LSM.SsoService.Application.Command.Requests.Auth;

[Validator<RegisterCommandValidator>]
public sealed record RegisterCommand(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTime BirthDate
) : ICommand;
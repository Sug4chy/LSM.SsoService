using LSM.SsoService.Application.Command.Interfaces;
using LSM.SsoService.Application.Command.Validation.Auth;
using LSM.SsoService.Application.Common.Validation.Attributes;

namespace LSM.SsoService.Application.Command.Requests.Auth;

[Validator<RegisterCommandValidator>]
public sealed record RegisterCommand(
    string Username,
    string Password,
    string Name,
    string Surname,
    DateTime BirthDate,
    string? Email = null,
    string? Patronymic = null
) : ICommand;
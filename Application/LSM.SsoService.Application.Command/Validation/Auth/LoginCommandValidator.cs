using FluentValidation;
using LSM.SsoService.Application.Command.Extensions;
using LSM.SsoService.Application.Command.Requests.Auth;

namespace LSM.SsoService.Application.Command.Validation.Auth;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Password)
            .Password();

        RuleFor(command => command.Email)
            .Email();
    }
}
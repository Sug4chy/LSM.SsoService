using FluentValidation;
using LSM.SsoService.Application.Command.Requests.Auth;

namespace LSM.SsoService.Application.Command.Validators.Auth;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Email)
            .EmailAddress()
            .WithMessage("Некорректный Email");
    }
}
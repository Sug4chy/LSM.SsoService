using FluentValidation;
using LSM.SsoService.Application.Command.Extensions;
using LSM.SsoService.Application.Command.Requests.Auth;
using static LSM.SsoService.Application.Command.Validation.ValidationMessages;

namespace LSM.SsoService.Application.Command.Validation.Auth;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Password)
            .Password();

        RuleFor(command => command.Name)
            .NotEmptyWithMessage(nameof(RegisterCommand.Name));

        RuleFor(command => command.Surname)
            .NotEmptyWithMessage(nameof(RegisterCommand.Surname));

        RuleFor(command => command.BirthDate)
            .GreaterThan(DateTime.Now.AddYears(-18))
            .WithMessage(TooYoung);

        RuleFor(command => command.Email)
            .Email();

        RuleFor(command => command.Patronymic)!
            .NotEmptyWithMessage(nameof(RegisterCommand.Name))
            .When(command => command.Patronymic is not null);
    }
}
using FluentValidation;
using LSM.SsoService.Application.Command.Requests.Auth;
using LSM.SsoService.Domain.ValueObjects;
using static LSM.SsoService.Application.Command.Validation.ValidationMessages;

namespace LSM.SsoService.Application.Command.Validation.Auth;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty()
            .WithMessage(
                string.Format(NotEmpty, nameof(RegisterCommand.Username))
            );

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage(
                string.Format(NotEmpty, nameof(RegisterCommand.Password))
            );

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage(
                string.Format(NotEmpty, nameof(RegisterCommand.Name))
            );

        RuleFor(command => command.Surname)
            .NotEmpty()
            .WithMessage(
                string.Format(NotEmpty, nameof(RegisterCommand.Surname))
            );

        RuleFor(command => command.BirthDate)
            .GreaterThan(DateTime.Now.AddYears(-18))
            .WithMessage(TooYoung);

        RuleFor(command => command.Email)
            .Must(Email.IsValid!)
            .When(command => command.Email is not null)
            .WithMessage(InvalidEmail);

        RuleFor(command => command.Patronymic)
            .NotEmpty()
            .When(command => command.Patronymic is not null)
            .WithMessage(
                string.Format(NotEmpty, nameof(RegisterCommand.Patronymic))
            );
    }
}
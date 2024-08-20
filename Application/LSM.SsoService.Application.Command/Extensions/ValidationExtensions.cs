using FluentValidation;
using LSM.SsoService.Application.Command.Validation;

namespace LSM.SsoService.Application.Command.Extensions;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .NotEmpty()
            .Must(s => s.Any(char.IsDigit)
                       && s.All(char.IsLetterOrDigit)
                       && s.Any(char.IsUpper)
                       && s.Length >= 8)
            .WithMessage(ValidationMessages.InvalidPassword);
    
    public static IRuleBuilderOptions<T, string> NotEmptyWithMessage<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        string propertyName)
        => ruleBuilder
            .NotEmpty()
            .WithMessage(
                string.Format(ValidationMessages.NotEmpty, propertyName)
            );
    
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
            .Must(Domain.ValueObjects.Email.IsValid)
            .WithMessage(ValidationMessages.InvalidEmail);
}
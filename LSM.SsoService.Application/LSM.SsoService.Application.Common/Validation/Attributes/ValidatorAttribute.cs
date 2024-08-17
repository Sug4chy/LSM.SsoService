using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Application.Common.Validation.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ValidatorAttribute<TValidator> : ValidationAttribute
    where TValidator : class, IValidator, new()
{
    private readonly ValidationResult _validatorTypeMismatch = new("Validator type mismatch");

    private static ValidationResult Failure(string errorMessage, string? memberName)
        => new($"[Validation error: {errorMessage}]",
            string.IsNullOrWhiteSpace(memberName) is false
                ? [memberName]
                : null);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var validator = validationContext.GetRequiredService<TValidator>();

        if (value is null || validator.CanValidateInstancesOfType(value.GetType()) is false)
        {
            return _validatorTypeMismatch;
        }

        var result = validator.Validate(new ValidationContext<object>(value));
        if (result.IsValid)
        {
            return ValidationResult.Success;
        }

        var validationFailure = result.Errors[0];
        return Failure(validationFailure.ErrorMessage, validationFailure.PropertyName);
    }
}
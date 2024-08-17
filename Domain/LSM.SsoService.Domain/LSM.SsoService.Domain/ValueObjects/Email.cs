using CSharpFunctionalExtensions;

namespace LSM.SsoService.Domain.ValueObjects;

public sealed class Email : SimpleValueObject<string>
{
    private Email(string value) : base(value)
    {
    }

    public static bool IsValid(string value)
        => string.IsNullOrWhiteSpace(value) is false
           && value.Count(c => c is '@') is 1
           && value.Count(c => c is '\r' or '\n') is 0
           && value.Split('@', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
               .Length is 2;

    public static Result<Email> Parse(string value)
        => IsValid(value)
            ? Result.Success(new Email(value))
            : Result.Failure<Email>("Некорректный формат Email");

    public override string ToString()
        => Value;
}
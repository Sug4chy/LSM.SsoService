using CSharpFunctionalExtensions;

namespace LSM.SsoService.Domain.ValueObjects;

public sealed class Email : SimpleValueObject<string>
{
    private Email(string value) : base(value)
    {
    }

    private static bool IsValid(string value)
        => string.IsNullOrWhiteSpace(value) is false
           && value.Count(c => c is '@') is 1
           && value.Count(c => c is '\r' or '\n') is 0;

    public static Result<Email> Parse(string value)
        => IsValid(value) is false
            ? Result.Failure<Email>("Некорректный формат Email")
            : Result.Success(new Email(value));

    public override string ToString()
        => Value;
}
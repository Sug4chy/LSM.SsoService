namespace LSM.SsoService.Application.Command.Validation;

public static class ValidationMessages
{
    public const string NotEmpty = "Поле {0} не должно быть пустым.";
    public const string TooYoung = "Слишком юный возраст, должен быть минимум 18.";
    public const string InvalidEmail = "Некорректный формат Email.";
}
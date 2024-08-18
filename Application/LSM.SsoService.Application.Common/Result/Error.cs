namespace LSM.SsoService.Application.Common.Result;

public readonly struct Error
{
    public ErrorGroup ErrorGroup { get; init; }
    public string Message { get; init; }

    private Error(ErrorGroup errorGroup, string message)
    {
        ErrorGroup = errorGroup;
        Message = message;
    }
    
    public static Error Create(ErrorGroup errorGroup, string message)
        => new(errorGroup, message);
}
namespace LSM.SsoService.Application.Common.Results;

public readonly struct Error
{
    public ErrorGroup ErrorGroup { get; }
    public string Message { get; }

    private Error(ErrorGroup errorGroup, string message)
    {
        ErrorGroup = errorGroup;
        Message = message;
    }
    
    public static Error Create(ErrorGroup errorGroup, string message)
        => new(errorGroup, message);
}
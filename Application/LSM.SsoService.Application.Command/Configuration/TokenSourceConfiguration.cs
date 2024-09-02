namespace LSM.SsoService.Application.Command.Configuration;

public sealed record TokenSourceConfiguration
{
    public const string Location = "TokenSource";
    
    public int ResetPasswordTokenExpiryDays { get; init; }
    public TimeSpan ResetPasswordTokenExpirationPeriod
        => TimeSpan.FromDays(ResetPasswordTokenExpiryDays);
}
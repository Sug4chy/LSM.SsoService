using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LSM.SsoService.Infrastructure.Tokens.Jwt.Configuration;

public sealed record JwtConfiguration
{
    public const string Location = "Jwt";
    
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
    public required int ExpireMinutes { get; init; }
    public TimeSpan ExpirationPeriod => TimeSpan.FromMinutes(ExpireMinutes);
    
    public TokenValidationParameters ToTokenValidationParameters()
        => new()
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
        };
}
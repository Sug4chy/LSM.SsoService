using LSM.SsoService.Domain.ValueObjects.Tokens;
using LSM.SsoService.Infrastructure.Tokens.Jwt.Services;
using LSM.SsoService.Infrastructure.Tokens.Jwt.Services.Implementations;
using LSM.SsoService.Infrastructure.Tokens.Services;
using LSM.SsoService.Infrastructure.Tokens.Services.Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Infrastructure.Tokens.Extensions;

public static class DependencyInjection
{
    public static AuthenticationBuilder WithJwtGenerator(this AuthenticationBuilder builder)
    {
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        
        return builder;
    }

    public static AuthenticationBuilder WithTokenSources(this AuthenticationBuilder builder)
    {
        builder.Services.AddScoped<ITokenSource<ResetPasswordToken>, ResetPasswordTokenSource>();
        
        return builder;
    }
}
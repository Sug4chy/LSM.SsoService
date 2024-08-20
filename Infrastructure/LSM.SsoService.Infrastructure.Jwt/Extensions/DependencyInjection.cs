using LSM.SsoService.Infrastructure.Jwt.Services;
using LSM.SsoService.Infrastructure.Jwt.Services.Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Infrastructure.Jwt.Extensions;

public static class DependencyInjection
{
    public static AuthenticationBuilder WithJwtServices(this AuthenticationBuilder builder)
    {
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        
        return builder;
    }
}
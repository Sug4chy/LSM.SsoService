using FluentValidation;
using LSM.SsoService.Application.Command.Handlers.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Application.Command.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandValidation(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<IValidationAssemblyMarker>();

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // Auth
        services.AddScoped<RegisterCommandHandler>();
        services.AddScoped<LoginCommandHandler>();
        
        return services;
    }
}

file interface IValidationAssemblyMarker;
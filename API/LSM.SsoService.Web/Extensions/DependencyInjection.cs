using LSM.SsoService.Application.Command.Extensions;
using Microsoft.Extensions.Options;

namespace LSM.SsoService.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRequestValidation(this IServiceCollection services)
        => services.AddCommandValidation();

    public static IServiceCollection AddSettings<TConfiguration>(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
        where TConfiguration : class
    {
        services.Configure<TConfiguration>(configurationSection);
        
        using var serviceProvider = services.BuildServiceProvider();
        
        var options = serviceProvider.GetRequiredService<IOptions<TConfiguration>>();
        services.AddSingleton(options.Value);

        return services;
    }
}
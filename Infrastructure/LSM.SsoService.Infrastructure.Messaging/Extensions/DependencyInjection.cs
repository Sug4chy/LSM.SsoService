using LSM.SsoService.Infrastructure.Messaging.Services;
using LSM.SsoService.Infrastructure.Messaging.Services.Implementations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Infrastructure.Messaging.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });

        services.AddScoped<IMessagingService, MessagingServiceClient>();

        return services;
    }
}
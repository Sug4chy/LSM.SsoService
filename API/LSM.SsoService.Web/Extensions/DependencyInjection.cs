using LSM.SsoService.Application.Command.Extensions;

namespace LSM.SsoService.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRequestValidation(this IServiceCollection services)
        => services.AddCommandValidation();
}
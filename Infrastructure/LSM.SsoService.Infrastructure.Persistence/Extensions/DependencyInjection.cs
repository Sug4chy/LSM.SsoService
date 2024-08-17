using LSM.SsoService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LSM.SsoService.Infrastructure.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string? connectionString)
        => services.AddDbContext<IDataContext, DataContext>(builder =>
        {
            builder.UseNpgsql(connectionString);
        });
}
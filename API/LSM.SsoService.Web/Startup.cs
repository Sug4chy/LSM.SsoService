using LSM.SsoService.Application.Command.Handlers.Auth;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using LSM.SsoService.Web.Extensions;

namespace LSM.SsoService.Web;

public sealed class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddRequestValidation();
        services.AddPersistence(configuration.GetConnectionString("DefaultConnection"));
        
        services.AddScoped<RegisterCommandHandler>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(builder => builder.MapControllers());
    }
}
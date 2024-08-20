using LSM.SsoService.Application.Command.Extensions;
using LSM.SsoService.Infrastructure.Jwt.Configuration;
using LSM.SsoService.Infrastructure.Jwt.Extensions;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using LSM.SsoService.Web.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

        services.Configure<JwtConfiguration>(configuration.GetSection(JwtConfiguration.Location));
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtConfig = configuration
                    .GetSection(JwtConfiguration.Location)
                    .Get<JwtConfiguration>();
                options.TokenValidationParameters = jwtConfig!.ToTokenValidationParameters();
            })
            .WithJwtServices();

        services.AddRequestValidation();
        services.AddPersistence(configuration.GetConnectionString("DefaultConnection"));

        services.AddCommandHandlers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(builder => builder.MapControllers());
    }
}
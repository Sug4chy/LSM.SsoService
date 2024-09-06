using LSM.SsoService.Application.Command.Extensions;
using LSM.SsoService.Infrastructure.Messaging.Extensions;
using LSM.SsoService.Infrastructure.Persistence.Extensions;
using LSM.SsoService.Infrastructure.Tokens.Configuration;
using LSM.SsoService.Infrastructure.Tokens.Extensions;
using LSM.SsoService.Infrastructure.Tokens.Jwt.Configuration;
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

        services.AddSettings<JwtConfiguration>(configuration.GetSection(JwtConfiguration.Location));
        services.AddSettings<TokenSourceConfiguration>(configuration.GetSection(TokenSourceConfiguration.Location));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtConfig = configuration
                    .GetSection(JwtConfiguration.Location)
                    .Get<JwtConfiguration>();
                options.TokenValidationParameters = jwtConfig!.ToTokenValidationParameters();
            })
            .WithJwtGenerator()
            .WithTokenSources();

        services.AddRequestValidation();
        services.AddPersistence(configuration.GetConnectionString("DefaultConnection"));
        services.AddCommandHandlers();

        services.AddMessaging();
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
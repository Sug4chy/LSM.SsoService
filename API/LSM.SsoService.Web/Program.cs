using LSM.SsoService.Web;

string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(hostBuilder =>
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();
        hostBuilder.UseConfiguration(configuration);
        hostBuilder.UseStartup<Startup>();
    })
    .UseEnvironment(environment ?? Environments.Development)
    .Build()
    .RunAsync();

// TODO миграция и поменять конфиг сущности User
// TODO добавить конфигурацию для TokenSource
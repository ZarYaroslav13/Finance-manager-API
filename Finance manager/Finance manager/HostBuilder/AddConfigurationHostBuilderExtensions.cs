namespace Finance_manager.HostBuilder;

public static class AddConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder Configure(this IHostApplicationBuilder hostBuilder)
    {
        var location = AppContext.BaseDirectory;
        var configuration = hostBuilder.Configuration;
        string environmentName = Environment.GetEnvironmentVariable("CORE_ENVIRONMENT") ?? "Development";
        Environment.SetEnvironmentVariable("BASEDIR", location);

        configuration.SetBasePath(location);
        configuration.AddJsonFile("appsettings.json");
        configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
        configuration.AddEnvironmentVariables();

        return hostBuilder;
    }
}

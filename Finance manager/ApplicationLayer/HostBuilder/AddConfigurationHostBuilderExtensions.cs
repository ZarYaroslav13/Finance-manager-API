namespace ApplicationLayer.HostBuilder;

public static class AddConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder Configure(this IHostApplicationBuilder hostBuilder)
    {
        const string environmentNameVariable = "ASPNETCORE_ENVIRONMENT";

        var location = AppContext.BaseDirectory;
        var configuration = hostBuilder.Configuration;
        string environmentName = Environment.GetEnvironmentVariable(environmentNameVariable) ?? "Development";
        Environment.SetEnvironmentVariable("BASEDIR", location);

        configuration.SetBasePath(location);
        configuration.AddJsonFile("appsettings.json");
        configuration.AddJsonFile($"appsettings.{environmentName}.json");
        configuration.AddEnvironmentVariables();

        return hostBuilder;
    }
}

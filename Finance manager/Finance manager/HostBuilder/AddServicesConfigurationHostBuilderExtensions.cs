namespace Finance_manager.HostBuilder;

public static class AddServicesConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder hostBuilder)
    {
        var services = hostBuilder.Services;

        return hostBuilder;
    }
}

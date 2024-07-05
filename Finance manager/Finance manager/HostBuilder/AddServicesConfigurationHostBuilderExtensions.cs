using DataLayer;
using DataLayer.UnitOfWork;

namespace Finance_manager.HostBuilder;

public static class AddServicesConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddDbContext<AppDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return builder;
    }
}

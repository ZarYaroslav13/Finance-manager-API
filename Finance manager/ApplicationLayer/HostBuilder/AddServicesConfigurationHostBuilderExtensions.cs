using DataLayer;
using DataLayer.UnitOfWork;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Wallets;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.HostBuilder;

public static class AddServicesConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {
        const string connectionString = "DbConnection";
        var services = builder.Services;
        var configuration = builder.Configuration as IConfiguration;

        services.AddDbContext<AppDbContext>(option =>
            option.
                UseSqlServer(
                    configuration.
                        GetConnectionString(connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IWalletService, WalletService>();

        return builder;
    }
}

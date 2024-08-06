﻿using DataLayer;
using DataLayer.UnitOfWork;
using DomainLayer.Services.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Finance_manager_API.HostBuilder;

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

        return builder;
    }
}

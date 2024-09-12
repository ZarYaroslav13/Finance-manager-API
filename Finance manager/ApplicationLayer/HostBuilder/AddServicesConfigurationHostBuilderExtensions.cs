using ApplicationLayer.Security;
using ApplicationLayer.Security.Jwt;
using DataLayer;
using DataLayer.Security;
using DataLayer.UnitOfWork;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ApplicationLayer.HostBuilder;

public static class AddServicesConfigurationHostBuilderExtensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration as IConfiguration;

        services.AddSingleton<IPasswordCoder, PasswordCoder>();

        services.AddDbConnection(configuration);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IFinanceService, FinanceService>();
        services.AddScoped<ITokenManager, TokenManager>();

        services.AddJwtAuthentication();

        services.AddPoliticalAuthorization(configuration);

        return builder;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = "DbConnection";

        services.AddDbContext<AppDbContext>(option =>
            option.
                UseSqlServer(
                    configuration.
                        GetConnectionString(connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = AuthOptions.ISSUER,
                            ValidAudience = AuthOptions.AUDIENCE,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        };
                    });

        return services;
    }

    private static IServiceCollection AddPoliticalAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();

        var adminService = serviceProvider.GetRequiredService<IAdminService>();

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("OnlyForAdmins", async policy =>
            {
                policy.RequireClaim(ClaimTypes.Name, adminService.GetAdmins().Select(a => a.Email));
            });
        });

        return services;
    }
}

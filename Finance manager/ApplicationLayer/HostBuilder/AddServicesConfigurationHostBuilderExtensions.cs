using ApplicationLayer.Security;
using ApplicationLayer.Security.Jwt;
using DataLayer;
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

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IFinanceService, FinanceService>();
        services.AddScoped<ITokenManager, TokenManager>();

        services.AddJwtAuthentication();

        services.AddPoliticalAuthorization(configuration);

        return builder;
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
            opt.AddPolicy("OnlyForAdmins", policy =>
            {
                policy.RequireClaim(ClaimTypes.Name, adminService.Admins.Select(a => a.Email));
            });
        });

        return services;
    }
}

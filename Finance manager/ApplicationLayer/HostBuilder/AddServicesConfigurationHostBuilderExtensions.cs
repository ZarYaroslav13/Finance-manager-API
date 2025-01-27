using ApplicationLayer.Routing;
using ApplicationLayer.Security;
using ApplicationLayer.Security.Jwt;
using Infrastructure;
using Infrastructure.Security;
using Infrastructure.UnitOfWork;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using DomainLayer.Services.Finances;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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

        builder.AddOptions();

        services.AddSingleton<IPasswordCoder, PasswordCoder>();

        services.AddDbConnection(configuration);

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IFinanceReportCreator, FinanceReportCreator>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IFinanceService, FinanceService>();
        services.AddScoped<ITokenManager, TokenManager>();

        services.AddJwtAuthentication(configuration);

        services.AddPoliticalAuthorization();

        services.AddControllers(options =>
            options.Conventions
                .Add(new RouteTokenTransformerConvention(
                        new SlugifyParameterTransformer())));

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

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        AuthOptions authOptions = configuration.GetSection(AuthOptions.Auth).Get<AuthOptions>();

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

                            ValidIssuer = authOptions.ISSUER,
                            ValidAudience = authOptions.AUDIENCE,

                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        };
                    });

        return services;
    }

    private static IServiceCollection AddPoliticalAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(AdminService.NameAdminPolicy, policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, AdminService.NameAdminRole);
            });
        });

        return services;
    }

    private static IHostApplicationBuilder AddOptions(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<AuthOptions>(
            builder.Configuration.GetSection(AuthOptions.Auth));

        return builder;
    }
}

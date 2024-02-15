using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Auth.Configuration;
using Store.Auth.Interfaces;
using Store.Auth.Services;

namespace Store.Auth.Extensions;

public static class DependencyInjection
{

    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var cfg = new JwtBearerConfiguration();
        configuration.Bind("JwtBearer", cfg);

        if (cfg.SecretKey.IsNullOrEmpty())
            throw new ArgumentException("SecretKey can't be null or empty");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = cfg.AuthenticationScheme;
            options.DefaultChallengeScheme = cfg.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = cfg.RequireHttpsMetadata;
            options.SaveToken = cfg.SaveToken;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = cfg.ValidateIssuer,
                ValidIssuer = cfg.ValidIssuer,
                ValidateAudience = cfg.ValidateAudience,
                ValidAudience = cfg.ValidAudience,
                ValidateIssuerSigningKey = cfg.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(cfg.SecretKey)),
                ValidateLifetime = cfg.ValidateLifetime,
                ClockSkew = cfg.ClockSkew
            };
        });

        return services;
    }

    public static IServiceCollection AddJwtBearerConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration["JwtBearer:SecretKey"] = configuration["JwtBearerSecretKey"];
        services.Configure<JwtBearerConfiguration>(configuration.GetSection("JwtBearer"));

        return services;
    }

    public static IServiceCollection AddAuthService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddUserRoleService(this IServiceCollection services)
    {
        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }

}
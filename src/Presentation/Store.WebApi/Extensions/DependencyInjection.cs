using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Store.Application.Common;
using Store.Auth.Interfaces;
using Store.WebApi.Common;
using Store.WebApi.Common.Validation.ProductController;

namespace Store.WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Roles.Owner, builder => { builder.RequireClaim(ClaimTypes.Role, Roles.Owner); });
            options.AddPolicy(Roles.Admin, builder =>
            {
                builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Admin) ||
                                              x.User.HasClaim(ClaimTypes.Role, Roles.Owner));
            });
            options.AddPolicy(Roles.User, builder =>
            {
                builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.User) ||
                                              x.User.HasClaim(ClaimTypes.Role, Roles.Admin) ||
                                              x.User.HasClaim(ClaimTypes.Role, Roles.Owner));
            });
        });
        return services;
    }

    public static IServiceCollection AddAuthContext(this IServiceCollection services)
    {
        services.AddScoped<IAuthContext, AuthContext>();
        return services;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(c => c.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));
        return services;
    }

    public static IServiceCollection AddSwaggerGenConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version = "v1" });
            option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }


    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<CreateProductCommandValidation>()
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }
}
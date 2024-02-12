using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Mapper;

namespace Store.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config => { config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly)); });
        return services;
    }

    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }

    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IValidationHandler>()
            .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }

    public static IServiceCollection AddValidatorBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}
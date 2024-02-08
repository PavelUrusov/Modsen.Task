using Microsoft.Extensions.DependencyInjection;
using Store.Application.Mapper;

namespace Store.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
        });
        return services;
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Entities;

namespace Store.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(opt =>
        {
            opt.UseLazyLoadingProxies().UseNpgsql(configuration["DbConnection"]);
        });
        services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<StoreDbContext>();

        return services;
    }
}
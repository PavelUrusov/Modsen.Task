using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.TransactionService;
using Store.Auth.Interfaces;
using Store.Domain.Entities;
using Store.Persistence.Repositories;
using Store.Persistence.Services;

namespace Store.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(opt => { opt.UseLazyLoadingProxies().UseNpgsql(configuration["DbConnection"]); });
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
        }).AddEntityFrameworkStores<StoreDbContext>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }

    public static IServiceCollection AddTransactionService(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
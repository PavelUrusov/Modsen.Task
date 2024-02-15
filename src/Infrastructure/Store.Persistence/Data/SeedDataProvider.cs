using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.Application.Common;
using Store.Domain.Entities;

namespace Store.Persistence.Data;

public static class SeedDataProvider
{

    public static async Task<IHost> SeedInitialDataAsync(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();
        await SeedInitialRolesAsync(roleManager, logger);
        await SeedInitialUsers(userManager, logger);
        await SeedCategoriesAsync(dbContext, logger);
        await SeedProductsAsync(dbContext, logger);

        return host;
    }

    private static async Task SeedInitialRolesAsync(RoleManager<Role> roleManager, ILogger<IHost> logger)
    {
        var methodName = nameof(SeedInitialRolesAsync);

        if (!roleManager.Roles.Any())
        {
            var roles = new List<Role>
            {
                new() { Name = Roles.User, Level = Roles.RoleLevels[Roles.User] },
                new() { Name = Roles.Employee, Level = Roles.RoleLevels[Roles.Employee] },
                new() { Name = Roles.Admin, Level = Roles.RoleLevels[Roles.Admin] },
                new() { Name = Roles.Owner, Level = Roles.RoleLevels[Roles.Owner] }
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);
            logger.LogInformation($"{methodName} - Roles seeded successfully.");
        }
        else
        {
            logger.LogDebug($"{methodName} - Roles are already seeded");
        }
    }

    private static async Task SeedInitialUsers(UserManager<User> userManager, ILogger<IHost> logger)
    {
        var methodName = nameof(SeedInitialUsers);

        if (!userManager.Users.Any())
        {
            var owner = new User { UserName = "owner" };
            var admin = new User { UserName = "admin" };
            var employee = new User { UserName = "employee" };
            var customer = new User { UserName = "customer" };
            await userManager.CreateAsync(owner, "owner123");
            await userManager.CreateAsync(admin, "admin123");
            await userManager.CreateAsync(employee, "employee123");
            await userManager.CreateAsync(customer, "customer123");
            await userManager.AddToRoleAsync(owner, Roles.Owner);
            await userManager.AddToRoleAsync(admin, Roles.Admin);
            await userManager.AddToRoleAsync(employee, Roles.Employee);
            await userManager.AddToRoleAsync(customer, Roles.User);
            logger.LogInformation($"{methodName} - Users seeded successfully.");
        }
        else
        {
            logger.LogDebug($"{methodName} - Users are already seeded");
        }
    }

    internal static async Task SeedCategoriesAsync(StoreDbContext dbContext, ILogger<IHost> logger)
    {
        var methodName = nameof(SeedCategoriesAsync);

        if (!dbContext.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { Name = "Electronics", Description = "Gadgets and electronic devices." },
                new() { Name = "Books", Description = "Various books and literature." }
            };

            dbContext.Categories.AddRange(categories);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"{methodName} - Categories seeded successfully.");
        }
        else
        {
            logger.LogDebug($"{methodName} - Categories are already seeded.");
        }
    }

    internal static async Task SeedProductsAsync(StoreDbContext dbContext, ILogger<IHost> logger)
    {
        var methodName = nameof(SeedProductsAsync);

        if (!dbContext.Products.Any())
        {
            var electronicsCategory = dbContext.Categories.FirstOrDefault(c => c.Name == "Electronics");
            var booksCategory = dbContext.Categories.FirstOrDefault(c => c.Name == "Books");

            var products = new List<Product>
            {
                new()
                {
                    Name = "Smartphone", Description = "Latest model smartphone.", Price = 999.99m, Quantity = 100,
                    Categories = new List<Category> { electronicsCategory }
                },
                new()
                {
                    Name = "Sci-Fi Book", Description = "A book about future.", Price = 19.99m, Quantity = 50,
                    Categories = new List<Category> { booksCategory }
                }
            };

            dbContext.Products.AddRange(products);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"{methodName} - Products seeded successfully.");
        }
        else
        {
            logger.LogDebug($"{methodName} - Products are already seeded.");
        }
    }

}
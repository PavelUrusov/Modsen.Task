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
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();
        await SeedInitialRolesAsync(roleManager, logger);
        await SeedInitialUsers(userManager, logger);

        return host;
    }

    private static async Task SeedInitialRolesAsync(RoleManager<IdentityRole<Guid>> roleManager, ILogger<IHost> logger)
    {
        var methodName = nameof(SeedInitialRolesAsync);

        if (!roleManager.Roles.Any())
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new(Roles.User),
                new(Roles.Employee),
                new(Roles.Admin),
                new(Roles.Owner)
            };

            foreach (var role in roles)
                await roleManager.CreateAsync(role);
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
            var user = new User { UserName = "owner" };
            await userManager.CreateAsync(user, "owner123");
            await userManager.AddToRoleAsync(user, Roles.Owner);
        }
        else
        {
            logger.LogDebug($"{methodName} - Users are already seeded");
        }
    }

}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class CategoryRepository : BaseRepository<Category, int, CategoryRepository>, ICategoryRepository
{

    public CategoryRepository(StoreDbContext dbContext, ILogger<CategoryRepository> logger) : base(dbContext, logger)
    {
    }

    public async Task<Category?> ReadByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadByNameAsync);

        try
        {
            Logger.LogDebug($"{methodName} - Starting to find a {EntityType} by Name.");
            var entity = await DbSet.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

            if (entity != null)
                Logger.LogDebug($"{methodName} - Successfully found a {EntityType} by Name.");
            else
                Logger.LogWarning($"{methodName} - {EntityType} by Name not found.");

            return entity;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to find a {EntityType} by Name. Error: {ex.Message}");

            throw;
        }
    }

}
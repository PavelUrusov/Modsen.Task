using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Interfaces;

namespace Store.Persistence.Repositories;

internal abstract class BaseRepository<TEntity, TKey, TRepository> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
    where TRepository : BaseRepository<TEntity, TKey, TRepository>
{
    protected readonly StoreDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;
    protected readonly string EntityType;
    protected readonly ILogger<TRepository> Logger;

    protected BaseRepository(StoreDbContext dbContext, ILogger<TRepository> logger)
    {
        DbContext = dbContext;
        Logger = logger;
        DbSet = DbContext.Set<TEntity>();
        EntityType = typeof(TEntity).Name;
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(CreateAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to create a new {EntityType}.");
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully created a new {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to create a new {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(CreateRangeAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to create multiple {EntityType} entities.");
            await DbSet.AddRangeAsync(entities, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully created multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to create multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(UpdateAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to update a {EntityType}.");
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully updated a {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - {methodName} - Failed to update a {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(UpdateRangeAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to update multiple {EntityType} entities.");
            DbSet.UpdateRange(entities);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully updated multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to update multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<TEntity?> ReadAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to find a {EntityType} by ID.");
            var entity = await DbSet.FindAsync(key);
            if (entity != null)
                Logger.LogDebug($"{methodName} - Successfully found a {EntityType} by ID.");
            else
                Logger.LogWarning($"{methodName} - {EntityType} by ID not found.");
            return entity;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to find a {EntityType} by ID. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadRangeAsync<TKeySelector>(int skip, int take,
        Expression<Func<TEntity, TKeySelector>> keySelector,
        IEnumerable<Expression<Func<TEntity, bool>>>? filters = default,
        CancellationToken cancellationToken = default) where TKeySelector : IComparable
    {
        var methodName = nameof(ReadRangeAsync);
        try
        {
            Logger.LogDebug(
                $"{methodName} - Starting to read a range of {EntityType} entities, skipping {skip} and taking {take}.");
            var query = DbSet.AsQueryable();
            if (filters != null)
                foreach (var filter in filters)
                    query = query.Where(filter);
            var entities = await query.OrderBy(keySelector).Skip(skip).Take(take).ToListAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully read a range of {EntityType} entities.");
            return entities;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to read a range of {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadAllAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to read all {EntityType} entities.");
            var entities = await DbSet.ToListAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully read all {EntityType} entities.");
            return entities;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to read all {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(DeleteAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to delete a {EntityType}.");
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully deleted a {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to delete a {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(DeleteRangeAsync);
        try
        {
            Logger.LogDebug($"{methodName} - Starting to delete multiple {EntityType} entities.");
            DbSet.RemoveRange(entities);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{methodName} - Successfully deleted multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to delete multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }
}
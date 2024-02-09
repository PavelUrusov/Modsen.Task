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
    protected readonly string ServiceName;

    protected BaseRepository(StoreDbContext dbContext, ILogger<TRepository> logger)
    {
        DbContext = dbContext;
        Logger = logger;
        DbSet = DbContext.Set<TEntity>();
        EntityType = typeof(TEntity).Name;
        ServiceName = typeof(TRepository).Name;
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(CreateAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to create a new {EntityType}.");
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully created a new {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{ServiceName}.{methodName} Failed to create a new {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(CreateRangeAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to create multiple {EntityType} entities.");
            await DbSet.AddRangeAsync(entities, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully created multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError(
                $"{ServiceName}.{methodName} Failed to create multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(UpdateAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to update a {EntityType}.");
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully updated a {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{ServiceName}.{methodName} Failed to update a {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(UpdateRangeAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to update multiple {EntityType} entities.");
            DbSet.UpdateRange(entities);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully updated multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError(
                $"{ServiceName}.{methodName} Failed to update multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<TEntity?> ReadAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to find a {EntityType} by ID.");
            var entity = await DbSet.FindAsync(key, cancellationToken);
            if (entity != null)
                Logger.LogDebug($"{ServiceName}.{methodName} Successfully found a {EntityType} by ID.");
            else
                Logger.LogWarning($"{ServiceName}.{methodName} {EntityType} by ID not found.");
            return entity;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{ServiceName}.{methodName} Failed to find a {EntityType} by ID. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadRangeAsync(int skip, int take,
        Expression<Func<TEntity, TKey>> keySelector,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadRangeAsync);
        try
        {
            Logger.LogDebug(
                $"{ServiceName}.{methodName} Starting to read a range of {EntityType} entities, skipping {skip} and taking {take}.");
            var entities = await DbSet.OrderBy(keySelector).Skip(skip).Take(take).ToListAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully read a range of {EntityType} entities.");
            return entities;
        }
        catch (Exception ex)
        {
            Logger.LogError(
                $"{ServiceName}.{methodName} Failed to read a range of {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ReadAllAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to read all {EntityType} entities.");
            var entities = await DbSet.ToListAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully read all {EntityType} entities.");
            return entities;
        }
        catch (Exception ex)
        {
            Logger.LogError(
                $"{ServiceName}.{methodName} Failed to read all {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(DeleteAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to delete a {EntityType}.");
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully deleted a {EntityType}.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"{ServiceName}.{methodName} Failed to delete a {EntityType}. Error: {ex.Message}");
            throw;
        }
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        var methodName = nameof(DeleteRangeAsync);
        try
        {
            Logger.LogDebug($"{ServiceName}.{methodName} Starting to delete multiple {EntityType} entities.");
            DbSet.RemoveRange(entities);
            await DbContext.SaveChangesAsync(cancellationToken);
            Logger.LogDebug($"{ServiceName}.{methodName} Successfully deleted multiple {EntityType} entities.");
        }
        catch (Exception ex)
        {
            Logger.LogError(
                $"{ServiceName}.{methodName} Failed to delete multiple {EntityType} entities. Error: {ex.Message}");
            throw;
        }
    }
}
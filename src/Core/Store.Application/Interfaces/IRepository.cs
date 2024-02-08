using System.Linq.Expressions;
using Store.Domain.Interfaces;

namespace Store.Application.Interfaces;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task<TEntity?> ReadAsync(TKey key, CancellationToken cancellationToken = default);

    public Task<IEnumerable<TEntity>> ReadRangeAsync(int skip, int take, Expression<Func<TEntity, TKey>> keySelector,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
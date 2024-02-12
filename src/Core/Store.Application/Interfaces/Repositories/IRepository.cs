using System.Linq.Expressions;
using Store.Domain.Interfaces;

namespace Store.Application.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task<TEntity?> ReadAsync(TKey key, CancellationToken cancellationToken = default);

    public Task<IEnumerable<TEntity>> ReadRangeAsync<TKeySelector>(int skip, int take,
        Expression<Func<TEntity, TKeySelector>> keySelector,
        IEnumerable<Expression<Func<TEntity, bool>>>? filters = default,
        CancellationToken cancellationToken = default) where TKeySelector : IComparable;

    public Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
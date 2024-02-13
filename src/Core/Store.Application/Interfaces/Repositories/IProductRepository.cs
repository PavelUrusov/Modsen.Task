using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
    public Task<Product?> ReadByNameAsync(string name, CancellationToken cancellation = default);
}
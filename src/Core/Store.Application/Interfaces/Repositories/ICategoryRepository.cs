using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category, int>
{
    public Task<Category?> ReadByNameAsync(string name, CancellationToken cancellationToken = default);
}
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

    public async Task<Category?> ReadByNameAsync(string name)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class ProductRepository : BaseRepository<Product, int, ProductRepository>, IProductRepository
{
    public ProductRepository(StoreDbContext dbContext, ILogger<ProductRepository> logger) : base(dbContext, logger)
    {
    }
}
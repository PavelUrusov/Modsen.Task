using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
}
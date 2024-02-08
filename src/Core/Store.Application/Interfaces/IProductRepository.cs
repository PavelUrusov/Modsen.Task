using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface IProductRepository : IRepository<Product, int>
{
}
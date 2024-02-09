using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category, int>
{
}
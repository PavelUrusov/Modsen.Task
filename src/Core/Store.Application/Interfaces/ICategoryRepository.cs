using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface ICategoryRepository : IRepository<Category, int>
{
}
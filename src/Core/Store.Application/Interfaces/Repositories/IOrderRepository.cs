using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order, int>
{
}
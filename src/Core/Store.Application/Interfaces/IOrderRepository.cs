using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface IOrderRepository : IRepository<Order, int>
{
}
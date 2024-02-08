using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem, int>
{
}
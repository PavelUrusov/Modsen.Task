using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order, int>
{

    Task<IEnumerable<Order>> ReadUserOrdersAsync(Guid userId, int take, int skip);
    Task<IEnumerable<Order>> ReadUserOrdersAsync(Guid userId);

}
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class OrderItemRepository : BaseRepository<OrderItem, int, OrderItemRepository>, IOrderItemRepository
{
    public OrderItemRepository(StoreDbContext dbContext, ILogger<OrderItemRepository> logger) : base(dbContext, logger)
    {
    }
}
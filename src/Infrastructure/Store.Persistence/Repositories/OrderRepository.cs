using Microsoft.Extensions.Logging;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class OrderRepository : BaseRepository<Order, int, OrderRepository>, IOrderRepository
{
    public OrderRepository(StoreDbContext dbContext, ILogger<OrderRepository> logger) : base(dbContext, logger)
    {
    }
}
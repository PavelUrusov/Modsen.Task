using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class OrderRepository : BaseRepository<Order, int, OrderRepository>, IOrderRepository
{

    public OrderRepository(StoreDbContext dbContext, ILogger<OrderRepository> logger) : base(dbContext, logger)
    {
    }

    public async Task<IEnumerable<Order>> ReadUserOrdersAsync(Guid userId, int take, int skip)
    {
        var methodName = nameof(ReadUserOrdersAsync);

        try
        {
            Logger.LogDebug($"{methodName} - Starting to find a orders by UserId.");
            var orders = await DbContext.Orders.Where(o => o.UserId == userId).Skip(skip).Take(take).ToListAsync();
            Logger.LogDebug($"{methodName} - Successfully found orders by UserId.");

            return orders;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to find orders by UserId. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<IEnumerable<Order>> ReadUserOrdersAsync(Guid userId)
    {
        var methodName = nameof(ReadUserOrdersAsync);

        try
        {
            Logger.LogDebug($"{methodName} - Starting to find a orders.");
            var orders = await DbSet.ToListAsync();
            Logger.LogDebug($"{methodName} - Successfully found orders.");

            return orders;
        }
        catch (Exception ex)
        {
            Logger.LogError($"{methodName} - Failed to find orders. Error: {ex.Message}");

            throw;
        }
    }

}
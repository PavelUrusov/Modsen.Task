using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces;

namespace Store.Persistence.Services;

internal class TransactionService : ITransactionService
{
    private readonly StoreDbContext _dbContext;
    private readonly ILogger<TransactionService> _logger;
    private readonly string _serviceName;

    public TransactionService(StoreDbContext dbContext, ILogger<TransactionService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _serviceName = nameof(TransactionService);
    }

    public async Task ExecuteInTransactionAsync(IEnumerable<Func<Task>> actions,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ExecuteInTransactionAsync);
        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        try
        {
            foreach (var action in actions) await action();
            await transaction.CommitAsync(cancellationToken);
            _logger.LogDebug($"{_serviceName}.{methodName}. Transaction {transaction.TransactionId} committed successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError($"{_serviceName}.{methodName}. Transaction rolled back due to an error. Error: {ex.Message}");
            throw;
        }
    }
}
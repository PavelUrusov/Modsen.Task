using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.Interfaces.TransactionService;

namespace Store.Persistence.Services;

internal class TransactionService : ITransactionService
{

    private readonly StoreDbContext _dbContext;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(StoreDbContext dbContext, ILogger<TransactionService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ExecuteInTransactionAsync);

        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

        try
        {
            await action();
            await transaction.CommitAsync(cancellationToken);

            _logger.LogDebug(
                $"{methodName} - Transaction {transaction.TransactionId} committed successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            _logger.LogError(
                $"{methodName} - Transaction rolled back due to an error. Error: {ex.Message}");

            throw;
        }
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        var methodName = nameof(ExecuteInTransactionAsync);

        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);

        try
        {
            var result = await action();
            await transaction.CommitAsync(cancellationToken);

            _logger.LogDebug(
                $"{methodName} - Transaction {transaction.TransactionId} committed successfully.");

            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            _logger.LogError(
                $"{methodName} - Transaction rolled back due to an error. Error: {ex.Message}");

            throw;
        }
    }

}
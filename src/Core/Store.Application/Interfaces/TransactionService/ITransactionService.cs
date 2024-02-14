using System.Data;

namespace Store.Application.Interfaces.TransactionService;

public interface ITransactionService
{

    Task ExecuteInTransactionAsync(Func<Task> action, IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default);

    public Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

}
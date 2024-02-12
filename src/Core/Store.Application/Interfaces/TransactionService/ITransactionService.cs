using System.Data;

namespace Store.Application.Interfaces.TransactionService;

public interface ITransactionService
{
    Task ExecuteInTransactionAsync(Func<Task> action, IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default);
}
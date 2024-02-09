using System.Data;

namespace Store.Application.Interfaces.TransactionService;

public interface ITransactionService
{
    Task ExecuteInTransactionAsync(IEnumerable<Func<Task>> actions, IsolationLevel isolationLevel,
        CancellationToken cancellationToken);
}
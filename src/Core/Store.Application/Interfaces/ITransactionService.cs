using System.Data;

namespace Store.Application.Interfaces;

public interface ITransactionService
{
    Task ExecuteInTransactionAsync(IEnumerable<Func<Task>> actions, IsolationLevel isolationLevel,
        CancellationToken cancellationToken);
}
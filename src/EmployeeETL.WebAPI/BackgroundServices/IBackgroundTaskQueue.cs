using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeETL.BackgroundServices;

public interface IBackgroundTaskQueue
{
    void Enqueue(Func<CancellationToken, ValueTask> task);
    Task<Func<CancellationToken, ValueTask>> Dequeue(CancellationToken cancellationToken);
}

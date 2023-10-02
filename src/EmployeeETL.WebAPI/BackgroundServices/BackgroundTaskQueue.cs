using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeETL.BackgroundServices;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly ConcurrentQueue<Func<CancellationToken, ValueTask>> _tasks;
    private readonly SemaphoreSlim _semaphore;


    public BackgroundTaskQueue()
    {
        _tasks = new ConcurrentQueue<Func<CancellationToken, ValueTask>>();
        _semaphore = new SemaphoreSlim(0);
    }

    public void Enqueue(Func<CancellationToken, ValueTask> task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        _tasks.Enqueue(task);
        _semaphore.Release();
    }

    public async Task<Func<CancellationToken, ValueTask>> Dequeue(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        _tasks.TryDequeue(out var task);

        if (task is null)
            return await Task.FromResult<Func<CancellationToken, ValueTask>>(null);

        return task;
    }
}

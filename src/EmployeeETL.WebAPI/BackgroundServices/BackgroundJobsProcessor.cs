using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeETL.BackgroundServices;

public class BackgroundJobsProcessor : BackgroundService
{
    private readonly IBackgroundTaskQueue _tasksQueue;
    private ILogger<BackgroundJobsProcessor> _logger;

    public BackgroundJobsProcessor(IBackgroundTaskQueue tasksQueue, ILogger<BackgroundJobsProcessor> logger)
    {
        _tasksQueue = tasksQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Add retry policy
        while (stoppingToken.IsCancellationRequested == false)
        {
            var task = await _tasksQueue.Dequeue(stoppingToken);
            _logger.LogInformation("New background task");
            try
            {
                if (task is not null)
                {
                    await task(stoppingToken);
                    _logger.LogInformation("Background task processed");
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Failed processing background task: {Message}", e.Message);
                throw e;
            }
        }
    }
}

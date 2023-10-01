using EmployeeETL.BackgroundServices;
using EmployeeETL.ETL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeETL.DependencyResolution;

public static class DependencyREsolutionConfigurator
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<IJobsService, JobsService>();
        builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

        return builder;
    }

    public static WebApplicationBuilder ConfigureHostedServices(this WebApplicationBuilder builder)
    {
        // Register as many as processors count (Environment.ProcessorCount) instances
        // to scale out the load.
        builder.Services.AddHostedService<BackgroundJobsProcessor>();
        return builder;
    }


}
using EmployeeETL.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeETL.DependencyResolution;

public static class DependencyREsolutionConfigurator
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<IJobsService, JobsService>();

        return builder;
    }
}
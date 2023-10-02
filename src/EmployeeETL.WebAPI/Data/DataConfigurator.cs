using System.Threading.Tasks;
using EmployeeETL.ETL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeETL.Data;

public static class DataConfigurator
{
    public static WebApplicationBuilder ConfigureETLStorage(this WebApplicationBuilder builder, string connectionStringName)
    {
        var connectionString = builder.Configuration.GetConnectionString(connectionStringName);
        builder.Services
            .AddSqlServer<EmployeeETLContext>(connectionString)
            .AddScoped<IJobsRepository, JobsRepository>();

        return builder;
    }

    public static WebApplicationBuilder ConfigureHRStorage(this WebApplicationBuilder builder, string connectionStringName)
    {
        var connectionString = builder.Configuration.GetConnectionString(connectionStringName);
        builder.Services.
            AddSqlServer<EmployeeHRContext>(connectionString)
            .AddScoped<IEmployeeLoader, EmployeeLoader>();
        return builder;
    }

    public static async Task<IApplicationBuilder> InitializeDbAsync(this IApplicationBuilder appBuilder)
    {
        using var scope = appBuilder.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeETLContext>();
        await dbContext.Database.MigrateAsync();

        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(DataConfigurator));
        logger.LogInformation(5, "Database migrations applied; database is ready");

        return appBuilder;
    }

}


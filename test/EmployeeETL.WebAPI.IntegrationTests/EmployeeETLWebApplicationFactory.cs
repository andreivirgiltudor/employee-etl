using EmployeeETL.Data;
using EmployeeETL.WebAPI.IntegrationTests.setup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EmployeeETL.WebAPI.IntegrationTests;

public class EmployeeETLWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly IConfigurationRoot _configuration;

    public DataSeeder DataSeeder { get; private set; }

    public EmployeeETLWebApplicationFactory() : base()
    {
        _configuration = new ConfigurationBuilder().AddUserSecrets<EmployeeETLWebApplicationFactory>().Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureTestServices(builder =>
        {
            builder.RemoveAll(typeof(DbContextOptions<EmployeeETLContext>));
            builder.AddSqlServer<EmployeeETLContext>(_configuration.GetConnectionString("EmployeeETLContext"));

            var serviceProvider = builder.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EmployeeETLContext>();

            DataSeeder = new DataSeeder(context);
        });

        builder.UseEnvironment("Development");
    }
}

using EmployeeETL.Data;
using EmployeeETL.DependencyResolution;
using EmployeeETL.WebAPI.Endpoints;
using EmployeeETL.WebAPI.OpenApi;
using EmployeeETL.WebAPI.Versioning;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureVersions()
    .ConfigureSwagger()
    .ConfigureStorage("EmployeeETLContext")
    .ConfigureServices()
    .ConfigureHostedServices()
    ;
var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseAuthorization();
app.MapJobsV1Endpoints();
app.UseSwaggerExplorer();
await app.InitializeDbAsync();

app.Run();

// Ugly hack to make integration tests passing
public partial class Program
{
}
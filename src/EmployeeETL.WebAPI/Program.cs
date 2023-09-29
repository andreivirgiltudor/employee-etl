using EmployeeETL.WebAPI.Endpoints;
using EmployeeETL.WebAPI.OpenApi;
using EmployeeETL.WebAPI.Versioning;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureVersions()
    .ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseAuthorization();
app.MapLoadV1Endpoints();
app.UseSwaggerExplorer();
app.Run();

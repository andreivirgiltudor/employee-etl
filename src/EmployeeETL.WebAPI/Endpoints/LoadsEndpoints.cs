using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EmployeeETL.WebAPI.Endpoints;
public static class LoadEndpoints
{
private const string GetLoadsV1Endpoint = "GetLoadsV1";

    public static WebApplication MapLoadV1Endpoints(this WebApplication app)
    {
        var loads = app
            .NewVersionedApi()
            .MapGroup("/api/loads")            
            .HasApiVersion(1.0)
            .WithOpenApi()
            .WithTags("Loads")
            ;

        loads.MapGet("/", () =>
        {
            return Results.Ok();
        })        
        .WithSummary("Get all loads")
        .WithDescription("Returns all loads, either processed or not")
        .MapToApiVersion(1.0)
        ;

        
        loads.MapGet("/{id}", (int id) =>
        {
            if (id == 1)
                return Results.Ok();
            return Results.NotFound();
        })
        .WithName(GetLoadsV1Endpoint)        
        .WithSummary("Get load by id")
        .WithDescription("Returns the load with specified id")
        .MapToApiVersion(1.0)
        ;

        loads.MapPost("/csv", () => {
            return Results.BadRequest();
        })
        .WithSummary("Create new load")
        .WithDescription("Creates a new load with given CSV input")
        .MapToApiVersion(1.0)
        ;

        return app;
    }
}
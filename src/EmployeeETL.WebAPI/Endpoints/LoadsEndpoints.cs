using Microsoft.AspNetCore.Http.HttpResults;

public static class LoadEndpoints
{
    public static WebApplication MapLoadEndpoints(this WebApplication app)
    {
        var loads = app.MapGroup("/api/loads");

        loads.MapGet("/", () =>
        {
            return Results.Ok();
        });

        
        loads.MapGet("/{id}", (int id) =>
        {
            if (id == 1)
                return Results.Ok();
            return Results.NotFound();
        });

        loads.MapPost("/", () => {
            return Results.BadRequest();
        });

        return app;
    }
}
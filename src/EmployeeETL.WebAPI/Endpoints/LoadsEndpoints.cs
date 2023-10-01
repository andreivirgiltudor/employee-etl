using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeETL.DTOs;
using EmployeeETL.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeETL.WebAPI.Endpoints;
public static class LoadEndpoints
{

    private static IList<EtlJob> _jobs = new List<EtlJob>();

    private const string GetJobV1Endpoint = "GetJobsV1";

    public static WebApplication MapJobsV1Endpoints(this WebApplication app)
    {
        var jobsEndpoints = app
            .NewVersionedApi()
            .MapGroup("/api/jobs")
            .HasApiVersion(1.0)
            .WithOpenApi()
            .WithTags("Jobs")
            ;

        jobsEndpoints.MapGet("/", async (IJobsService jobsService) =>
        {
            var jobs = await jobsService.GetAllJobsAsync();
            var jobsDto = jobs.Select(j => j.ToDto());
            return Results.Ok(jobsDto);
        })
        .WithSummary("Get all jobs")
        .WithDescription("Returns all jobs")
        .MapToApiVersion(1.0)
        ;


        jobsEndpoints.MapGet("/{id}", async Task<Results<Ok<EtlJobDto>, NotFound>> (Guid id, IJobsService jobsService) =>
        {
            var job = await jobsService.GetJobAsync(id);
            if (job is null)
                return TypedResults.NotFound();
            return TypedResults.Ok(job.ToDto());
        })
        .WithName(GetJobV1Endpoint)
        .WithSummary("Get job by id")
        .WithDescription("Returns the job with specified id")
        .MapToApiVersion(1.0)
        ;

        jobsEndpoints.MapPost("/csv", async Task<Results<AcceptedAtRoute<EtlJobDto>, BadRequest>>(IFormFile importFile, IJobsService jobsSerive) =>
        {
            if (importFile.Length == 0)
                return TypedResults.BadRequest();

            var csvFilePath = Path.GetTempFileName();
            using (var csvFileStream = File.OpenWrite(csvFilePath))
            {
                await importFile.CopyToAsync(csvFileStream);
            }

            var job = await jobsSerive.CreateNewCSVJobAsync(csvFilePath);
            await jobsSerive.ProcessJob(job);
            return TypedResults.AcceptedAtRoute(job.ToDto(), GetJobV1Endpoint, new { id = job.Id });


        })
        .WithSummary("Create new job")
        .WithDescription("Creates a new job with given CSV input")
        .MapToApiVersion(1.0)
        ;

        return app;
    }
}
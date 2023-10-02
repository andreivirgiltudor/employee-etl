using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmployeeETL.DTOs;
using FluentAssertions;
using Xunit;

namespace EmployeeETL.WebAPI.IntegrationTests;

public class JobsTests : IClassFixture<EmployeeETLWebApplicationFactory>
{
    private readonly EmployeeETLWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public JobsTests(EmployeeETLWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_Should_Return_All_Jobs()
    {
        // setup
        await _factory.DataSeeder.RemoveAllJobs();
        var firstJob = await _factory.DataSeeder.CreateSampleJob();
        var secondJob = await _factory.DataSeeder.CreateSampleJob();

        // act
        var response = await _client.GetAsync("/api/jobs");

        // verify
        response.EnsureSuccessStatusCode();
        var jobs = await response.Content.ReadFromJsonAsync<IEnumerable<EtlJobDto>>();
        jobs.Should()
            .NotBeEmpty()
            .And.HaveCount(2)
            .And.Contain(c => c.Id == firstJob)
            .And.Contain(c => c.Id == secondJob);
    }

    [Fact]
    public async Task Get_Should_Return_Job_When_Job_With_Given_Id_Exists()
    {
        // setup
        await _factory.DataSeeder.RemoveAllJobs();
        var jobId = await _factory.DataSeeder.CreateSampleJob();

        // act
        var response = await _client.GetAsync($"/api/jobs/{jobId}");

        // verify
        response.EnsureSuccessStatusCode();
        var job = await response.Content.ReadFromJsonAsync<EtlJobDto>();
        job?.Should().NotBeNull();
        job?.Id.Should().Be(jobId);
    }

    [Fact]
    public async Task Get_Should_Return_NotFound_When_Job_With_Given_Id_Does_Not_Exist()
    {
        // setup
        await _factory.DataSeeder.RemoveAllJobs();
        var fakeId = Guid.NewGuid();

        // act
        var response = await _client.GetAsync($"/api/jobs/{fakeId}");

        // verify
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_With_Empty_CSV_File_Should_Return_Bad_Request()
    {
        // setup
        await _factory.DataSeeder.RemoveAllJobs();
        using var formContent = new MultipartFormDataContent();
        var filePath = $"{Directory.GetCurrentDirectory()}/SampleData/CSV/empty.csv";
        var fileContent = new StreamContent(File.OpenRead(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
        formContent.Add(fileContent, name: "importFile", fileName: "employee.csv");

        // act
        var response = await _client.PostAsync("/api/jobs/csv", formContent);

        // verify
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_With_Valid_CSV_File_Should_Load_Employee()
    {
        // setup
        await _factory.DataSeeder.RemoveAllJobs();
        using var formContent = new MultipartFormDataContent();
        var filePath = $"{Directory.GetCurrentDirectory()}/SampleData/CSV/valid.csv";
        var fileContent = new StreamContent(File.OpenRead(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
        formContent.Add(fileContent, name: "importFile", fileName: "employee.csv");

        // act
        var response = await _client.PostAsync("/api/jobs/csv", formContent);

        // verify
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var job = await response.Content.ReadFromJsonAsync<EtlJobDto>();
        job?.Should().NotBeNull();

        var location = response.Headers.Location;
        location?.AbsolutePath.Should().Be($"/api/jobs/{job?.Id}");        
    }
}

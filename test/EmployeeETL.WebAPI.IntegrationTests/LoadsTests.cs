using System.Net;
using FluentAssertions;

namespace EmployeeETL.WebAPI.IntegrationTests;

public class LoadsTests
{
    private readonly EmployeeETLWebApplicationFactory _app;
    private readonly HttpClient _client;

    public LoadsTests()
    {
        _app = new EmployeeETLWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task Get_Should_Return_200_OK()
    {
        // act
        var response = await _client.GetAsync("/api/loads");

        // verify
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_With_Id_Should_Return_200_Ok_For_Existing_Load()
    {
        // act
        var response = await _client.GetAsync("/api/loads/1");

        // verify
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Get_With_Id_Should_Return_404_NotFound_For_Unexisting_Load()
    {
        // act
        var response = await _client.GetAsync("/api/loads/100");

// verify
response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Posting_With_Empty_Body_Should_Return_Bad_Request()
    {
        // act
        var response = await _client.PostAsync("/api/loads", null);

        // verify
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

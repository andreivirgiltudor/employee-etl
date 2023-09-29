using System.Linq;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeETL.WebAPI.OpenApi;

internal class SwaggerGenConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerGenConfigurationOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        _provider.ApiVersionDescriptions.ToList().ForEach(apiVersion =>
        {
            options.SwaggerDoc(apiVersion.GroupName, new OpenApiInfo()
            {
                Title = $"Employee ETL API {apiVersion.ApiVersion}",
                Version = apiVersion.ApiVersion.ToString(),
                Description = "Provides basic load features"
            });
        });
    }
}
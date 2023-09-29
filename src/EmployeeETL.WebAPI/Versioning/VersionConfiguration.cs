using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeETL.WebAPI.Versioning;

public static class VersionConfiguration
{
    public static WebApplicationBuilder ConfigureVersions(this WebApplicationBuilder builder)
    {
        builder.Services
        .AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ApiVersionReader = new HeaderApiVersionReader("ApiVersion");
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
        });

        return builder;
    }
}
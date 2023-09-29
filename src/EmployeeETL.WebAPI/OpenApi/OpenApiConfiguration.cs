using System.Runtime.CompilerServices;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeETL.WebAPI.OpenApi;


public static class OpenApiConfigurations
{
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSwaggerGen()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenConfigurationOptions>()
            .AddEndpointsApiExplorer()
            ;
        return builder;
    }

    public static WebApplication UseSwaggerExplorer(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt => {
                app.DescribeApiVersions().ToList().ForEach(desc => {
                    var url = $"/swagger/{desc.GroupName}/swagger.json";
                    var name = desc.GroupName.ToUpperInvariant();
                    opt.SwaggerEndpoint(url, name);
                });
            });
        }

        return app;
    }
}

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
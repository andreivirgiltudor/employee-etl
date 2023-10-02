using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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

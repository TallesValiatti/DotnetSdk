using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Constants = DotnetSdk.Common.Constants;

namespace DotnetSdk.Api;

public static class Extensions
{
    public static WebApplicationBuilder AddServices(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddSingleton<Repository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.UseInlineDefinitionsForEnums();

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = Constants.ApiKeyHeader,
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });
            var key = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            };
            var requirement = new OpenApiSecurityRequirement
            {
                { key, new List<string>() }
            };
            c.AddSecurityRequirement(requirement);
        });

        builder.Services.AddScoped<AiService.AiService>();

        return builder;
    }

    public static WebApplication UseServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", " Dotnet SDK API");
            c.InjectStylesheet("/swagger/custom.css");
            c.RoutePrefix = string.Empty;
        });
        
        app.UseHttpsRedirection();
        app.UseAppEndpoints();
        
        return app;
    }
}
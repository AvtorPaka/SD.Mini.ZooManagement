using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.OpenApi.Models;
using SD.Mini.ZooManagement.Api.Extensions;
using SD.Mini.ZooManagement.Api.FilterAttributes;
using SD.Mini.ZooManagement.Api.Middleware;
using SD.Mini.ZooManagement.Application.DependencyInjection.Extensions;
using SD.Mini.ZooManagement.Infrastructure.DependencyInjection.Extensions;

namespace SD.Mini.ZooManagement.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddDalInfrastructure()
            .AddDalRepositories()
            .AddDomainServices()
            .AddGlobalFilters()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddMvcOptions(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.AddServer(new OpenApiServer
                {
                    Url = "/api/sd-zoo",
                    Description = "API Base Path"
                });
                
                options.CustomSchemaIds(x => x.FullName);
                
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        app.UseMiddleware<TracingMiddleware>();
        app.UsePathBase("/api/sd-zoo");
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/api/sd-zoo/swagger/v1/swagger.json", "SD.Mini.ZooManagement.Api v1");
        });
        
        app.UseRouting();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}
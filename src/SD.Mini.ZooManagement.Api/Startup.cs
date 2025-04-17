using System.Text.Json;
using FluentValidation;
using SD.Mini.ZooManagement.Api.Extensions;
using SD.Mini.ZooManagement.Api.FilterAttributes;
using SD.Mini.ZooManagement.Api.Middleware;

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
            .AddGlobalFilters()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            })
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(o => o.CustomSchemaIds(x => x.FullName));
    }

    public void Configure(IApplicationBuilder app)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        app.UseMiddleware<TracingMiddleware>();
        app.UsePathBase("/api/sd-zoo");
        
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseRouting();

        app.UseMiddleware<LoggingMiddleware>();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}
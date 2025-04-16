using System.Text.Json;

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
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            })
            .AddMvcOptions(options =>
            {
                // TODO: Add exception filter
            })
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(o => o.CustomSchemaIds(x => x.FullName));
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UsePathBase("/api/sd-zoo");
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.UseRouting();

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });
    }
}
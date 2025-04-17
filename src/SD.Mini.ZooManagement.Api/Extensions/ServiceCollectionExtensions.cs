using SD.Mini.ZooManagement.Api.FilterAttributes;

namespace SD.Mini.ZooManagement.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddGlobalFilters(this IServiceCollection services)
    {
        services.AddScoped<ExceptionFilter>();

        return services;
    }
}
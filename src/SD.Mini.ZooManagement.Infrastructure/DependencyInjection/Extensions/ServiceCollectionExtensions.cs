using Microsoft.Extensions.DependencyInjection;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;
using SD.Mini.ZooManagement.Infrastructure.Dal.Repositories;

namespace SD.Mini.ZooManagement.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryStorage>();

        return services;
    }

    public static IServiceCollection AddDalRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAnimalsRepository, AnimalsRepository>();
        services.AddScoped<IEnclosureRepository, EnclosureRepository>();
        services.AddScoped<IFeedingScheduleRepository, FeedingScheduleRepository>();
        
        return services;
    }
}
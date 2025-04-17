using Microsoft.Extensions.DependencyInjection;
using SD.Mini.ZooManagement.Application.Services;
using SD.Mini.ZooManagement.Application.Services.Interfaces;

namespace SD.Mini.ZooManagement.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAnimalService, AnimalService>();
        services.AddScoped<IAnimalTransferService, AnimalTransferService>();
        services.AddScoped<IEnclosureService, EnclosureService>();
        
        return services;
    }
}
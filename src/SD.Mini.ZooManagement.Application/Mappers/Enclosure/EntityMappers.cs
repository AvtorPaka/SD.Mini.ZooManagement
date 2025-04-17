using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;

namespace SD.Mini.ZooManagement.Application.Mappers.Enclosure;

internal static class EntityMappers
{
    internal static EnclosureModelContainer MapEntityToContainer(this EnclosureEntity entity)
    {
        return new EnclosureModelContainer(
            Id: entity.Id,
            AnimalsId: entity.AnimalsId,
            EnclosureModel: entity.MapEntityToModel()
        );
    }
    
    internal static IReadOnlyList<EnclosureModelContainer> MapEntitiesToContainers(this IReadOnlyList<EnclosureEntity> entities)
    {
        return entities.Select(e => e.MapEntityToContainer()).ToList();
    }

    internal static EnclosureModel MapEntityToModel(this EnclosureEntity entity)
    {
        return new EnclosureModel(
            type: entity.Type,
            volume: entity.Volume,
            maximumCapacity: entity.MaximumCapacity,
            currentCapacity: entity.CurrentCapacity
        );
    }
}
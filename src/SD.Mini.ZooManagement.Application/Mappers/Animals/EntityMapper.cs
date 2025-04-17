using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Animal;

namespace SD.Mini.ZooManagement.Application.Mappers.Animals;

internal static class EntityMapper
{
    internal static AnimalModelContainer MapEntityToContainer(this AnimalEntity entity)
    {
        return new AnimalModelContainer(
            AnimalModel: entity.MapEntityToModel(),
            AnimalId: entity.Id,
            EnclosureId: entity.EnclosureId
        );
    }
    
    internal static IReadOnlyList<AnimalModelContainer> MapEntitiesToContainers(this IReadOnlyList<AnimalEntity> entities)
    {
        return entities.Select(e => e.MapEntityToContainer()).ToList();
    }

    internal static AnimalModel MapEntityToModel(this AnimalEntity entity)
    {
        return new AnimalModel(
            type: entity.Type,
            birthday: entity.Birthday,
            nickname: entity.Nickname,
            gender: entity.Gender,
            favouriteFood: entity.FavouriteFood,
            healthCondition: entity.HealthCondition
        );
    }
}
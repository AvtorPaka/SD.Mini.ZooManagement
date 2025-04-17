using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule;

namespace SD.Mini.ZooManagement.Application.Mappers.FeedingSchedule;

internal static class EntityMappers
{
    internal static FeedingScheduleModelContainer MapEntityToContainer(this FeedingScheduleEntity entity)
    {
        return new FeedingScheduleModelContainer(
            Id: entity.Id,
            AnimalId: entity.AnimalId,
            FeedTime: entity.FeedingTime,
            FoodType: entity.FoodType,
            IsDone: entity.IsDone
        );
    }

    internal static IReadOnlyList<FeedingScheduleModelContainer> MapEntitiesToContainers(
        this IReadOnlyList<FeedingScheduleEntity> entities)
    {
        return entities.Select(e => e.MapEntityToContainer()).ToList();
    }

    internal static FeedingScheduleModel MapEntityToModel(this FeedingScheduleEntity entity, AnimalModel animalModel)
    {
        return new FeedingScheduleModel(
            animal: animalModel,
            feedingTime: entity.FeedingTime,
            foodType: entity.FoodType,
            isDone: entity.IsDone
        );
    }
}
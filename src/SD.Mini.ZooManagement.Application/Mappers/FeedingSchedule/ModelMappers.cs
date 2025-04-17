using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Mappers.FeedingSchedule;

internal static class ModelMappers
{
    internal static FeedingScheduleEntity MapModelToEntity(this FeedingScheduleModel model, EntityId animalId)
    {
        return new FeedingScheduleEntity
        {
            AnimalId = animalId,
            FeedingTime = model.FeedingTime,
            FoodType = model.FoodType,
            IsDone = model.IsDone
        };
    }
    
    internal static FeedingScheduleEntity MapModelToEntity(this FeedingScheduleModel model, EntityId animalId, EntityId id)
    {
        return new FeedingScheduleEntity
        {
            Id = id,
            AnimalId = animalId,
            FeedingTime = model.FeedingTime,
            FoodType = model.FoodType,
            IsDone = model.IsDone
        };
    }
}
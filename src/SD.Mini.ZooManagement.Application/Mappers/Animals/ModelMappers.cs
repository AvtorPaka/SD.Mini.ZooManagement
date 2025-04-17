using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Mappers.Animals;

internal static class ModelMappers
{
    internal static AnimalEntity MapModelToEntity(this AnimalModel model, EntityId? enclosureId = null)
    {
        return new AnimalEntity
        {
            Birthday = model.Birthday,
            EnclosureId = enclosureId,
            FavouriteFood = model.FavouriteFood,
            Gender = model.Gender,
            HealthCondition = model.HealthCondition,
            Nickname = model.Nickname,
            Type = model.Type
        };
    }
    
    internal static AnimalEntity MapModelToEntity(this AnimalModel model, EntityId id, EntityId? enclosureId)
    {
        return new AnimalEntity
        {
            Id = id,
            Birthday = model.Birthday,
            EnclosureId = enclosureId,
            FavouriteFood = model.FavouriteFood,
            Gender = model.Gender,
            HealthCondition = model.HealthCondition,
            Nickname = model.Nickname,
            Type = model.Type
        };
    }
}
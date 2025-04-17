using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Mappers.Enclosure;

internal static class ModelMappers
{
    internal static EnclosureEntity MapModelToEntity(this EnclosureModel model)
    {
        return new EnclosureEntity
        {
            Type = model.Type,
            CurrentCapacity = model.CurrentCapacity,
            MaximumCapacity = model.MaximumCapacity,
            Volume = model.Volume
        };
    }
    
    internal static EnclosureEntity MapModelToEntity(this EnclosureModel model, EntityId id)
    {
        return new EnclosureEntity
        {
            Id = id,
            Type = model.Type,
            CurrentCapacity = model.CurrentCapacity,
            MaximumCapacity = model.MaximumCapacity,
            Volume = model.Volume
        };
    }
}
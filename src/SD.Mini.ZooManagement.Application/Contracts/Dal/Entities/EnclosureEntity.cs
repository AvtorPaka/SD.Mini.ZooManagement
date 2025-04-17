using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;

public class EnclosureEntity
{
    public EntityId Id { get; init; } = new();
    
    // Ugly imitation of table references in sql databases, alike EF Core
    public List<EntityId> AnimalsId { get; init; } = [];
    
    public EnclosureType Type { get; init; }
    public decimal Volume { get; init; }
    public uint MaximumCapacity { get; init; }
    public uint CurrentCapacity { get; set; }
}
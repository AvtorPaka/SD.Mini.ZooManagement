using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;

public class EnclosureEntity
{
    public required EntityId Id { get; init; } = new();
    
    // Ugly imitation of table references in sql databases, alike EF Core
    public List<EntityId> Animals { get; init; } = [];
    
    public EnclosureType Type { get; init; }
    public decimal Volume { get; init; }
    public uint MaximumCapacity { get; init; }
    public uint CurrentCapacity { get; set; }
}
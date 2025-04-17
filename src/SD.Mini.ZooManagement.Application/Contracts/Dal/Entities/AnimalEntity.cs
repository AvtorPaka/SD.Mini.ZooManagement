using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;

public class AnimalEntity
{
    public EntityId Id { get; } = new();
    
    // Ugly imitation of table references in sql databases, alike EF Core
    public EntityId? EnclosureId { get; set; }
    
    public AnimalType Type { get; init; }
    public DateTime Birthday { get; init; }
    public string Nickname { get; init; } = string.Empty;
    public AnimalGender Gender { get; init; }
    public string FavouriteFood { get; init; } = string.Empty;
    public AnimalHealthCondition HealthCondition { get; set; }
}
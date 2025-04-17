using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;

public class FeedingScheduleEntity
{
    public EntityId Id { get; init; } = new();
    
    // Ugly imitation of table references in sql databases, alike EF Core
    public required EntityId AnimalId { get; init; }
    
    public TimeOnly FeedingTime { get; init; }
    public FoodType FoodType { get; init; }
    public bool IsDone { get; set; }
}
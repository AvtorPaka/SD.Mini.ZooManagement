using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Containers;

public record CreateFeedingScheduleContainer(
    EntityId AnimalId,
    TimeOnly FeedTime,
    FoodType FoodType
);
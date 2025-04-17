using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses.FeedingSchedule;

public record GetAnimalFeedingScheduleResponse(
    string Id,
    TimeOnly FeedTime,
    FoodType FoodType,
    bool IsDone
);
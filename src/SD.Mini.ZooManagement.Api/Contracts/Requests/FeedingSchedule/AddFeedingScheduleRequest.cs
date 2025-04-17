using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Requests.FeedingSchedule;

public record AddFeedingScheduleRequest(
    string AnimalId,
    TimeOnly FeedingTime,
    FoodType FoodType
);
using SD.Mini.ZooManagement.Api.Contracts.Requests.FeedingSchedule;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Api.Mappers.FeedingSchedule;

internal static class RequestMappers
{
    internal static CreateFeedingScheduleContainer MapRequestToContainer(this AddFeedingScheduleRequest request)
    {
        return new CreateFeedingScheduleContainer(
            AnimalId: new EntityId(request.AnimalId),
            FeedTime: request.FeedingTime,
            FoodType: request.FoodType
        );
    }
}
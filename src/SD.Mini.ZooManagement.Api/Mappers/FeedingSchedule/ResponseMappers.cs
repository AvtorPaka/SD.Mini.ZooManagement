using SD.Mini.ZooManagement.Api.Contracts.Responses.FeedingSchedule;
using SD.Mini.ZooManagement.Application.Containers;

namespace SD.Mini.ZooManagement.Api.Mappers.FeedingSchedule;

internal static class ResponseMappers
{
    internal static GetFeedingScheduleResponse MapContainerToGeneralResponse(this FeedingScheduleModelContainer container)
    {
        return new GetFeedingScheduleResponse(
            Id: container.Id.ToString(),
            AnimalId: container.AnimalId.ToString(),
            FeedTime: container.FeedTime,
            FoodType: container.FoodType,
            IsDone: container.IsDone
        );
    }
    
    internal static IEnumerable<GetFeedingScheduleResponse> MapContainersToGeneralResponses(this IReadOnlyList<FeedingScheduleModelContainer> containers)
    {
        return containers.Select(c => c.MapContainerToGeneralResponse());
    }
    
    internal static GetAnimalFeedingScheduleResponse MapContainerToPersonalResponse(this FeedingScheduleModelContainer container)
    {
        return new GetAnimalFeedingScheduleResponse(
            Id: container.Id.ToString(),
            FeedTime: container.FeedTime,
            FoodType: container.FoodType,
            IsDone: container.IsDone
        );
    }
    
    internal static IEnumerable<GetAnimalFeedingScheduleResponse> MapContainersToPersonalResponses(this IReadOnlyList<FeedingScheduleModelContainer> containers)
    {
        return containers.Select(c => c.MapContainerToPersonalResponse());
    }
}
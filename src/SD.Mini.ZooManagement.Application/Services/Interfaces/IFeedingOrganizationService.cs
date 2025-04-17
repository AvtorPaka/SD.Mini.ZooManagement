using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services.Interfaces;

public interface IFeedingOrganizationService
{
    public Task<EntityId> AddFeedingSchedule(CreateFeedingScheduleContainer container,
        CancellationToken cancellationToken);
    
    public Task<FeedingScheduleModelContainer> GetFeedingSchedule(EntityId id, CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<FeedingScheduleModelContainer>> GetFeedingSchedules(CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<FeedingScheduleModelContainer>> GetAnimalFeedingSchedule(EntityId animalId,
        CancellationToken cancellationToken);

    public Task MarkFeedingScheduleDone(EntityId id, CancellationToken cancellationToken);
}
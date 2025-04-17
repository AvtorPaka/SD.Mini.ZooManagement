using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;

public interface IFeedingScheduleRepository: IDbRepository
{
    public Task<EntityId> AddSchedule(FeedingScheduleEntity entity, CancellationToken cancellationToken);
    public Task<FeedingScheduleEntity> GetScheduleById(EntityId id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<FeedingScheduleEntity>> GetAllSchedules(CancellationToken cancellationToken);
    public Task<IReadOnlyList<FeedingScheduleEntity>> GetAnimalSchedules(EntityId animalId,
        CancellationToken cancellationToken);
    public Task UpdateSchedule(FeedingScheduleEntity updatedEntity, CancellationToken cancellationToken);
    public Task DeleteByAnimalId(EntityId animalId, CancellationToken cancellationToken);
}
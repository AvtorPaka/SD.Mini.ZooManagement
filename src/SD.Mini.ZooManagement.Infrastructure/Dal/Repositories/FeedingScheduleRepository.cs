using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Domain.Models.Val;
using SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;

namespace SD.Mini.ZooManagement.Infrastructure.Dal.Repositories;

public class FeedingScheduleRepository : BaseRepository, IFeedingScheduleRepository
{
    public FeedingScheduleRepository(InMemoryStorage memoryStorage) : base(memoryStorage)
    {
    }

    public async Task<EntityId> AddSchedule(FeedingScheduleEntity entity, CancellationToken cancellationToken)
    {
        MemoryStorage.FeedingScheduleEntities.Add(entity);

        return await Task.FromResult(entity.Id);
    }

    public async Task<FeedingScheduleEntity> GetScheduleById(EntityId id, CancellationToken cancellationToken)
    {
        var entity = MemoryStorage.FeedingScheduleEntities.FirstOrDefault(e => e.Id == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", id);
        }

        return await Task.FromResult(entity);
    }

    public async Task<IReadOnlyList<FeedingScheduleEntity>> GetAllSchedules(CancellationToken cancellationToken)
    {
        return await Task.FromResult(MemoryStorage.FeedingScheduleEntities);
    }

    public async Task<IReadOnlyList<FeedingScheduleEntity>> GetAnimalSchedules(EntityId animalId,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(MemoryStorage.FeedingScheduleEntities.Where(e => e.AnimalId == animalId).ToList());
    }

    public async Task UpdateSchedule(FeedingScheduleEntity updatedEntity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D

        var entity = MemoryStorage.FeedingScheduleEntities.FirstOrDefault(e => e.Id == updatedEntity.Id);
        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", updatedEntity.Id);
        }

        entity.IsDone = updatedEntity.IsDone;
    }

    public async Task DeleteByAnimalId(EntityId animalId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D
        MemoryStorage.FeedingScheduleEntities.RemoveAll(e => e.AnimalId == animalId);
    }
}
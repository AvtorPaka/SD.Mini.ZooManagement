using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Domain.Models.Val;
using SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;

namespace SD.Mini.ZooManagement.Infrastructure.Dal.Repositories;

public class EnclosureRepository: BaseRepository, IEnclosureRepository
{
    public EnclosureRepository(InMemoryStorage memoryStorage) : base(memoryStorage)
    {
    }

    public async Task<EntityId> AddEnclosure(EnclosureEntity entity, CancellationToken cancellationToken)
    {
        MemoryStorage.EnclosureEntities.Add(entity);

        return await Task.FromResult(entity.Id);
    }

    public async Task<EnclosureEntity> GetEnclosureById(EntityId id, CancellationToken cancellationToken)
    {
        var entity = MemoryStorage.EnclosureEntities.FirstOrDefault(e => e.Id == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", id);
        }

        return await Task.FromResult(entity);
    }

    public async Task<IReadOnlyList<EnclosureEntity>> GetAllEnclosures(CancellationToken cancellationToken)
    {
        return await Task.FromResult(MemoryStorage.EnclosureEntities);
    }

    public async Task DeleteEnclosureById(EntityId id, CancellationToken cancellationToken)
    {
        int deletedCount = MemoryStorage.EnclosureEntities.RemoveAll(e => e.Id == id);

        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D
        if (deletedCount == 0)
        {
            throw new EntityNotFoundException("Entity couldn't be found", id);
        }
    }

    public async Task DeleteEnclosureAnimal(EnclosureEntity updatedEntity, EntityId animalId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D

        var entity = MemoryStorage.EnclosureEntities.FirstOrDefault(e => e.Id == updatedEntity.Id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", updatedEntity.Id);
        }

        entity.CurrentCapacity = updatedEntity.CurrentCapacity;
        entity.AnimalsId.RemoveAll(i => i == animalId);
    }

    public async Task AddEnclosureAnimal(EnclosureEntity updatedEntity, EntityId animalId, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D

        var entity = MemoryStorage.EnclosureEntities.FirstOrDefault(e => e.Id == updatedEntity.Id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", updatedEntity.Id);
        }

        entity.CurrentCapacity = updatedEntity.CurrentCapacity;
        entity.AnimalsId.Add(animalId);
    }
}
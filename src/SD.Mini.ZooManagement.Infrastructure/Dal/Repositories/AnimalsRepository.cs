using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Domain.Models.Val;
using SD.Mini.ZooManagement.Infrastructure.Dal.Infrastructure;

namespace SD.Mini.ZooManagement.Infrastructure.Dal.Repositories;

public class AnimalsRepository : BaseRepository, IAnimalsRepository
{
    public AnimalsRepository(InMemoryStorage memoryStorage) : base(memoryStorage)
    {
    }

    public async Task<EntityId> AddAnimal(AnimalEntity entity, CancellationToken cancellationToken)
    {
        MemoryStorage.AnimalEntities.Add(entity);

        return await Task.FromResult(entity.Id);
    }

    public async Task<AnimalEntity> GetAnimalById(EntityId id, CancellationToken cancellationToken)
    {
        var entity = MemoryStorage.AnimalEntities.FirstOrDefault(e => e.Id == id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", id);
        }

        return await Task.FromResult(entity);
    }

    public async Task<IReadOnlyList<AnimalEntity>> GetAnimals(CancellationToken cancellationToken)
    {
        return await Task.FromResult(MemoryStorage.AnimalEntities);
    }

    public async Task DeleteAnimalById(EntityId id, CancellationToken cancellationToken)
    {
        int deletedCount = MemoryStorage.AnimalEntities.RemoveAll(e => e.Id == id);

        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D
        if (deletedCount == 0)
        {
            throw new EntityNotFoundException("Entity couldn't be found", id);
        }
    }

    public async Task UpdateAnimal(AnimalEntity updatedEntity, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D
        var entity = MemoryStorage.AnimalEntities.FirstOrDefault(e => e.Id == updatedEntity.Id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity couldn't be found", updatedEntity.Id);
        }

        entity.HealthCondition = updatedEntity.HealthCondition;
        entity.EnclosureId = updatedEntity.EnclosureId;
    }

    public async Task DeleteAnimalsEnclosureId(EntityId enclosureId,
        CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken); // Fiction ;D
        MemoryStorage.AnimalEntities.Where(e => e.EnclosureId == enclosureId).ToList()
            .ForEach(e => e.EnclosureId = null);
    }
}
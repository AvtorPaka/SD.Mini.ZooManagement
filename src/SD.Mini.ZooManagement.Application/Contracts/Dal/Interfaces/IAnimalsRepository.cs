using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;

public interface IAnimalsRepository: IDbRepository
{
    public Task<EntityId> AddAnimal(AnimalEntity entity, CancellationToken cancellationToken);
    public Task<AnimalEntity> GetAnimalById(EntityId id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<AnimalEntity>> GetAnimals(CancellationToken cancellationToken);
    public Task DeleteAnimalById(EntityId id, CancellationToken cancellationToken);
}
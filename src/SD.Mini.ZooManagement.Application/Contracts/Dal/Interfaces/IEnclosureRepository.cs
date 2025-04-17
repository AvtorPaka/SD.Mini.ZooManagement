using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;

public interface IEnclosureRepository: IDbRepository
{
    public Task<EntityId> AddEnclosure(EnclosureEntity entity, CancellationToken cancellationToken);
    public Task<EnclosureEntity> GetEnclosureById(EntityId id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<EnclosureEntity>> GetAllEnclosures(CancellationToken cancellationToken);
    public Task DeleteEnclosureById(EntityId id, CancellationToken cancellationToken);
    public Task DeleteEnclosureAnimal(EnclosureEntity updatedEntity, EntityId animalId, CancellationToken cancellationToken);

    public Task AddEnclosureAnimal(EnclosureEntity updatedEntity, EntityId animalId,
        CancellationToken cancellationToken);
}
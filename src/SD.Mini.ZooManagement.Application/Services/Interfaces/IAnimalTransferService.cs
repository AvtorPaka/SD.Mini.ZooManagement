using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services.Interfaces;

public interface IAnimalTransferService
{
    public Task TransferAnimalToEnclosure(EntityId animalId, EntityId newEnclosureId, CancellationToken cancellationToken);
    public Task RemoveAnimalFromEnclosure(EntityId animalId, CancellationToken cancellationToken);
    public Task RemoveAnimalFromEnclosure(EntityId enclosureId, EntityId animalId, CancellationToken cancellationToken);
}
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services.Interfaces;

public interface IAnimalService
{
    public Task<EntityId> AddNewAnimal(AnimalModel animalModel, CancellationToken cancellationToken);
    public Task<AnimalModelContainer> GetAnimal(EntityId id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<AnimalModelContainer>> GetAnimals(CancellationToken cancellationToken);
    public Task DeleteAnimal(EntityId id, CancellationToken cancellationToken);
    public Task HealAnimal(EntityId id, CancellationToken cancellationToken);
}
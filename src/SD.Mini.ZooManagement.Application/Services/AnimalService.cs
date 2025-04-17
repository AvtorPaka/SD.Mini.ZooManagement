using FluentValidation;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Application.Mappers.Animals;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Application.Validators;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public async Task<EntityId> AddNewAnimal(AnimalModel animalModel, CancellationToken cancellationToken)
    {
        try
        {
            return await AddNewAnimalUnsafe(animalModel, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new ApplicationValidationException("Invalid request parameters.", ex);
        }
    }

    private async Task<EntityId> AddNewAnimalUnsafe(AnimalModel animalModel, CancellationToken cancellationToken)
    {
        var validator = new AnimalModelValidator();
        await validator.ValidateAndThrowAsync(animalModel, cancellationToken);
        
        using var transaction = _animalsRepository.CreateTransactionScope();

        EntityId createdId = await _animalsRepository.AddAnimal(
            entity: animalModel.MapModelToEntity(),
            cancellationToken: cancellationToken
        );
        
        transaction.Complete();

        return createdId;
    }

    public async Task<AnimalModelContainer> GetAnimal(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            return await GetAnimalUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task<AnimalModelContainer> GetAnimalUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();
        
        AnimalEntity entity = await _animalsRepository.GetAnimalById(
            id: id,
            cancellationToken: cancellationToken
        );
        
        transaction.Complete();

        return entity.MapEntityToContainer();
    }

    public async Task<IReadOnlyList<AnimalModelContainer>> GetAnimals(CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();
        
        IReadOnlyList<AnimalEntity> entities = await _animalsRepository.GetAnimals(cancellationToken);
        
        transaction.Complete();

        return entities.MapEntitiesToContainers();
    }

    public async Task DeleteAnimal(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            await DeleteAnimalUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }
    
    private async Task DeleteAnimalUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}
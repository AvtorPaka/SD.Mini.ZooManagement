using FluentValidation;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Application.Mappers.Animals;
using SD.Mini.ZooManagement.Application.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Application.Validators;
using SD.Mini.ZooManagement.Domain.Exceptions.Animal;
using SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalsRepository _animalsRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalService(IAnimalsRepository animalsRepository, IEnclosureRepository enclosureRepository)
    {
        _animalsRepository = animalsRepository;
        _enclosureRepository = enclosureRepository;
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

    public async Task HealAnimal(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            await HealAnimalUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
        catch (AnimalAlreadyHealthyException ex)
        {
            throw new AnimalHealDuplicateException($"Animal with id: {id} already healthy.", ex);
        }
    }

    private async Task HealAnimalUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();

        AnimalEntity entity = await _animalsRepository.GetAnimalById(id, cancellationToken);

        AnimalModel model = entity.MapEntityToModel();

        model.Heal();

        await _animalsRepository.UpdateAnimal(
            updatedEntity: model.MapModelToEntity(
                id: entity.Id,
                enclosureId: entity.EnclosureId
            ),
            cancellationToken: cancellationToken
        );

        transaction.Complete();
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
        catch (EnclosureCapacityException ex)
        {
            throw new EnclosureCapacityConflictException(
                message: $"Associated with animal: {id} enclosure already empty.",
                isOverfill: false,
                ex
            );
        }
    }

    private async Task DeleteAnimalUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();

        AnimalEntity animalEntity = await _animalsRepository.GetAnimalById(id, cancellationToken);

        if (animalEntity.EnclosureId != null)
        {
            EnclosureEntity enclosureEntity = await _enclosureRepository.GetEnclosureById(
                id: animalEntity.EnclosureId,
                cancellationToken: cancellationToken
            );

            EnclosureModel enclosureModel = enclosureEntity.MapEntityToModel();
            enclosureModel.DecreaseCurrentCapacity();

            await _enclosureRepository.DeleteEnclosureAnimal(
                updatedEntity: enclosureModel.MapModelToEntity(enclosureEntity.Id),
                animalId: animalEntity.Id,
                cancellationToken: cancellationToken
            );
        }

        await _animalsRepository.DeleteAnimalById(
            id: id,
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }
}
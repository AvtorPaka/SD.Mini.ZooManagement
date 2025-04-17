using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Application.Mappers.Animals;
using SD.Mini.ZooManagement.Application.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services;

public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalsRepository _animalsRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalTransferService(IAnimalsRepository animalsRepository, IEnclosureRepository enclosureRepository)
    {
        _animalsRepository = animalsRepository;
        _enclosureRepository = enclosureRepository;
    }

    public async Task RemoveAnimalFromEnclosure(EntityId animalId, CancellationToken cancellationToken)
    {
        try
        {
            await RemoveAnimalFromEnclosureUnsafe(animalId, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }
    
    private async Task RemoveAnimalFromEnclosureUnsafe(EntityId animalId, CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();
        
        AnimalEntity animalEntity = await _animalsRepository.GetAnimalById(animalId, cancellationToken);

        if (animalEntity.EnclosureId == null)
        {
            throw new AnimalTransferValidationException(
                $"Animal with id: {animalId} doesnt belong to any enclosure");
        }
        
        await RemoveAnimalFromOldEnclosure(
            oldEnclosureId: animalEntity.EnclosureId,
            animalId: animalId,
            cancellationToken: cancellationToken
        );

        animalEntity.EnclosureId = null;

        await _animalsRepository.UpdateAnimal(
            updatedEntity: animalEntity,
            cancellationToken: cancellationToken
        );
        
        transaction.Complete();
    }

    public async Task RemoveAnimalFromEnclosure(EntityId enclosureId, EntityId animalId,
        CancellationToken cancellationToken)
    {
        try
        {
            await RemoveAnimalFromEnclosureUnsafe(enclosureId, animalId, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task RemoveAnimalFromEnclosureUnsafe(EntityId enclosureId, EntityId animalId,
        CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();

        AnimalEntity animalEntity = await _animalsRepository.GetAnimalById(animalId, cancellationToken);

        if (animalEntity.EnclosureId != enclosureId)
        {
            throw new AnimalTransferValidationException(
                $"Animal with id: {animalId} doesnt belong to enclosure: {enclosureId}");
        }

        await RemoveAnimalFromOldEnclosure(
            oldEnclosureId: enclosureId,
            animalId: animalId,
            cancellationToken: cancellationToken
        );

        animalEntity.EnclosureId = null;

        await _animalsRepository.UpdateAnimal(
            updatedEntity: animalEntity,
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }

    public async Task TransferAnimalToEnclosure(EntityId animalId, EntityId enclosureId,
        CancellationToken cancellationToken)
    {
        try
        {
            await TransferAnimalToEnclosureUnsafe(animalId, enclosureId, cancellationToken);
        }
        catch (EnclosureInvalidAnimalTypeException ex)
        {
            throw new AnimalTransferValidationException(
                $"Animal type: {ex.AnimalType} is incompatible with new enclosure type: {ex.InvalidEnclosureType}");
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task TransferAnimalToEnclosureUnsafe(EntityId animalId, EntityId newEnclosureId,
        CancellationToken cancellationToken)
    {
        using var transaction = _animalsRepository.CreateTransactionScope();

        AnimalEntity animalEntity = await _animalsRepository.GetAnimalById(animalId, cancellationToken);


        EntityId? oldEnclosureId = animalEntity.EnclosureId;

        if (oldEnclosureId == newEnclosureId)
        {
            throw new AnimalTransferValidationException("New enclosure must be different from current enclosure.");
        }

        if (oldEnclosureId != null)
        {
            await RemoveAnimalFromOldEnclosure(
                oldEnclosureId: oldEnclosureId,
                animalId: animalId,
                cancellationToken: cancellationToken
            );
        }

        AnimalModel animalModel = animalEntity.MapEntityToModel();

        await AddAnimalToNewEnclosure(
            newEnclosureId: newEnclosureId,
            animalId: animalId,
            animalModel: animalModel,
            cancellationToken: cancellationToken
        );

        await _animalsRepository.UpdateAnimal(
            updatedEntity: animalModel.MapModelToEntity(animalId, newEnclosureId),
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }

    private async Task AddAnimalToNewEnclosure(EntityId newEnclosureId, EntityId animalId, AnimalModel animalModel,
        CancellationToken cancellationToken)
    {
        try
        {
            await AddAnimalToNewEnclosureUnsafe(newEnclosureId, animalId, animalModel, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EnclosureNotFoundException($"Enclosure with id: ${ex.InvalidId} not found.", ex);
        }
        catch (EnclosureCapacityException ex)
        {
            throw new EnclosureCapacityConflictException(
                message: $"Enclosure with id: {newEnclosureId} is filled",
                isOverfill: true,
                ex
            );
        }
    }

    private async Task AddAnimalToNewEnclosureUnsafe(EntityId newEnclosureId, EntityId animalId,
        AnimalModel animalModel, CancellationToken cancellationToken)
    {
        EnclosureModel newEnclosureModel =
            (await _enclosureRepository.GetEnclosureById(newEnclosureId, cancellationToken))
            .MapEntityToModel();

        newEnclosureModel.IncreaseCurrentCapacity(animalModel);

        await _enclosureRepository.AddEnclosureAnimal(
            updatedEntity: newEnclosureModel.MapModelToEntity(newEnclosureId),
            animalId: animalId,
            cancellationToken: cancellationToken
        );
    }

    private async Task RemoveAnimalFromOldEnclosure(EntityId oldEnclosureId, EntityId animalId,
        CancellationToken cancellationToken)
    {
        try
        {
            await RemoveAnimalFromOldEnclosureUnsafe(oldEnclosureId, animalId, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EnclosureNotFoundException($"Enclosure with id: ${ex.InvalidId} not found.", ex);
        }
        catch (EnclosureCapacityException ex)
        {
            throw new EnclosureCapacityConflictException(
                message: $"Enclosure with id: {oldEnclosureId} is already empty.",
                isOverfill: false,
                ex
            );
        }
    }

    private async Task RemoveAnimalFromOldEnclosureUnsafe(EntityId oldEnclosureId, EntityId animalId,
        CancellationToken cancellationToken)
    {
        EnclosureModel oldEnclosureModel =
            (await _enclosureRepository.GetEnclosureById(oldEnclosureId, cancellationToken)).MapEntityToModel();

        oldEnclosureModel.DecreaseCurrentCapacity();

        await _enclosureRepository.DeleteEnclosureAnimal(
            updatedEntity: oldEnclosureModel.MapModelToEntity(oldEnclosureId),
            animalId: animalId,
            cancellationToken: cancellationToken
        );
    }
}
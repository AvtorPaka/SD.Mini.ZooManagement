using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;
using SD.Mini.ZooManagement.Application.Exceptions.Application.FeedingSchedules;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Application.Mappers.Animals;
using SD.Mini.ZooManagement.Application.Mappers.FeedingSchedule;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Exceptions.FeedingSchedule;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services;

public class FeedingOrganizationService : IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IAnimalsRepository _animalsRepository;

    public FeedingOrganizationService(IFeedingScheduleRepository feedingScheduleRepository,
        IAnimalsRepository animalsRepository)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _animalsRepository = animalsRepository;
    }

    public async Task<EntityId> AddFeedingSchedule(CreateFeedingScheduleContainer container,
        CancellationToken cancellationToken)
    {
        try
        {
            return await AddFeedingScheduleUnsafe(container, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
        catch (FeedingScheduleInvalidFoodTypeException ex)
        {
            throw new FeedingScheduleValidationException(
                $"Schedule food type: {ex.InvalidFoodType} is incompatible with animal type: {ex.AnimalType}", ex);
        }
    }

    private async Task<EntityId> AddFeedingScheduleUnsafe(CreateFeedingScheduleContainer container,
        CancellationToken cancellationToken)
    {
        using var transaction = _feedingScheduleRepository.CreateTransactionScope();

        AnimalEntity animalEntity = await _animalsRepository.GetAnimalById(
            id: container.AnimalId,
            cancellationToken: cancellationToken
        );

        FeedingScheduleModel model = new FeedingScheduleModel(
            Animal: animalEntity.MapEntityToModel(),
            FeedingTime: container.FeedTime,
            FoodType: container.FoodType
        );

        model.ValidateFoodType();

        FeedingScheduleEntity entity = model.MapModelToEntity(
            animalId: animalEntity.Id
        );

        await _feedingScheduleRepository.AddSchedule(
            entity: entity,
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        return entity.Id;
    }

    public async Task<FeedingScheduleModelContainer> GetFeedingSchedule(EntityId id,
        CancellationToken cancellationToken)
    {
        try
        {
            return await GetFeedingScheduleUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new FeedingScheduleNotFoundException($"Feeding schedule with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task<FeedingScheduleModelContainer> GetFeedingScheduleUnsafe(EntityId id,
        CancellationToken cancellationToken)
    {
        using var transaction = _feedingScheduleRepository.CreateTransactionScope();

        FeedingScheduleEntity entity = await _feedingScheduleRepository.GetScheduleById(
            id: id,
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        return entity.MapEntityToContainer();
    }

    public async Task<IReadOnlyList<FeedingScheduleModelContainer>> GetFeedingSchedules(
        CancellationToken cancellationToken)
    {
        using var transaction = _feedingScheduleRepository.CreateTransactionScope();
        
        var entities = await _feedingScheduleRepository.GetAllSchedules(cancellationToken);

        transaction.Complete();
        
        return entities.MapEntitiesToContainers();
    }

    public async Task<IReadOnlyList<FeedingScheduleModelContainer>> GetAnimalFeedingSchedule(EntityId animalId,
        CancellationToken cancellationToken)
    {
        using var transaction = _feedingScheduleRepository.CreateTransactionScope();
        
        var entities = await _feedingScheduleRepository.GetAnimalSchedules(
            animalId: animalId,
            cancellationToken: cancellationToken
        );
        
        transaction.Complete();

        return entities.MapEntitiesToContainers();
    }

    public async Task MarkFeedingScheduleDone(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            await MarkFeedingScheduleDoneUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            if (ex.InvalidId == id)
            {
                throw new FeedingScheduleNotFoundException($"Feeding schedule with id: ${ex.InvalidId} not found.", ex);
            }

            throw new AnimalNotFoundException($"Animal with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task MarkFeedingScheduleDoneUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _feedingScheduleRepository.CreateTransactionScope();
        
        var scheduleEntity = await _feedingScheduleRepository.GetScheduleById(
            id: id,
            cancellationToken: cancellationToken
        );

        var animalEntity = await _animalsRepository.GetAnimalById(
            id: scheduleEntity.AnimalId,
            cancellationToken: cancellationToken
        );

        FeedingScheduleModel model = scheduleEntity.MapEntityToModel(
            animalModel: animalEntity.MapEntityToModel()
        );

        model.MarkDone();

        await _feedingScheduleRepository.UpdateSchedule(
            updatedEntity: model.MapModelToEntity(animalEntity.Id, scheduleEntity.Id),
            cancellationToken: cancellationToken
        );
        
        transaction.Complete();
    }
}
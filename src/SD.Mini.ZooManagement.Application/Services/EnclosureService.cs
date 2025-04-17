using FluentValidation;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Exceptions.Application;
using SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;
using SD.Mini.ZooManagement.Application.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Application.Validators;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services;

public class EnclosureService : IEnclosureService
{
    private readonly IEnclosureRepository _enclosureRepository;
    private readonly IAnimalsRepository _animalsRepository;

    public EnclosureService(IEnclosureRepository enclosureRepository, IAnimalsRepository animalsRepository)
    {
        _enclosureRepository = enclosureRepository;
        _animalsRepository = animalsRepository;
    }

    public async Task<EntityId> AddNewEnclosure(EnclosureModel model, CancellationToken cancellationToken)
    {
        try
        {
            return await AddNewEnclosureUnsafe(model, cancellationToken);
        }
        catch (ValidationException ex)
        {
            throw new ApplicationValidationException("Invalid request parameters", ex);
        }
    }

    private async Task<EntityId> AddNewEnclosureUnsafe(EnclosureModel model, CancellationToken cancellationToken)
    {
        var validator = new EnclosureModelValidator();
        await validator.ValidateAndThrowAsync(model, cancellationToken);

        using var transaction = _enclosureRepository.CreateTransactionScope();

        EntityId createdId = await _enclosureRepository.AddEnclosure(
            entity: model.MapModelToEntity(),
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        return createdId;
    }

    public async Task<EnclosureModelContainer> GetEnclosure(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            return await GetEnclosureUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EnclosureNotFoundException($"Enclosure with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task<EnclosureModelContainer> GetEnclosureUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _enclosureRepository.CreateTransactionScope();

        var entity = await _enclosureRepository.GetEnclosureById(
            id: id,
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        return entity.MapEntityToContainer();
    }

    public async Task<IReadOnlyList<EnclosureModelContainer>> GetEnclosures(CancellationToken cancellationToken)
    {
        using var transaction = _enclosureRepository.CreateTransactionScope();

        var entities = await _enclosureRepository.GetAllEnclosures(cancellationToken);

        transaction.Complete();

        return entities.MapEntitiesToContainers();
    }

    public async Task DeleteEnclosure(EntityId id, CancellationToken cancellationToken)
    {
        try
        {
            await DeleteEnclosureUnsafe(id, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            throw new EnclosureNotFoundException($"Enclosure with id: ${ex.InvalidId} not found.", ex);
        }
    }

    private async Task DeleteEnclosureUnsafe(EntityId id, CancellationToken cancellationToken)
    {
        using var transaction = _enclosureRepository.CreateTransactionScope();

        await _animalsRepository.DeleteAnimalsEnclosureId(
            enclosureId: id,
            cancellationToken: cancellationToken
        );

        await _enclosureRepository.DeleteEnclosureById(
            id: id,
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }
}
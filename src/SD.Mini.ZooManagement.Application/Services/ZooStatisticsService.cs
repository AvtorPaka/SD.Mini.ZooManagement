using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Containers.Statistics;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Entities;
using SD.Mini.ZooManagement.Application.Contracts.Dal.Interfaces;
using SD.Mini.ZooManagement.Application.Mappers.Enclosure;
using SD.Mini.ZooManagement.Application.Services.Interfaces;
using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Application.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalsRepository _animalsRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsService(
        IAnimalsRepository animalsRepository,
        IEnclosureRepository enclosureRepository)
    {
        _animalsRepository = animalsRepository;
        _enclosureRepository = enclosureRepository;
    }

    public async Task<CountAnimalsTypeContainer> CountAnimalsTypes(CancellationToken cancellationToken)
    {
        IReadOnlyList<AnimalEntity> entities = await _animalsRepository.GetAnimals(cancellationToken);

        Dictionary<AnimalType, int> typesCountDict =
            entities
                .GroupBy(e => e.Type)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );

        foreach (var type in Enum.GetValues<AnimalType>())
        {
            typesCountDict.TryAdd(type, 0);
        }

        return new CountAnimalsTypeContainer(
            TotalCount: entities.Count,
            TypeCountDict: typesCountDict
        );
    }

    public async Task<decimal> CalculateAnimalEnclosurePlacementRate(CancellationToken cancellationToken)
    {
        IReadOnlyList<AnimalEntity> entities = await _animalsRepository.GetAnimals(cancellationToken);

        int totalCount = entities.Count;
        if (totalCount == 0)
        {
            return 0;
        }

        int inEnclosureCount = entities.Count(e => e.EnclosureId != null);


        return ((decimal)inEnclosureCount * 100) / totalCount;
    }

    public async Task<GetEnclosuresContainer> GetEmptyEnclosures(CancellationToken cancellationToken)
    {
        var entities = await _enclosureRepository.GetAllEnclosures(cancellationToken);

        var emptyEnclosureEntities = entities.Where(e => e.CurrentCapacity == 0).ToList();

        IReadOnlyList<EnclosureModelContainer> containers = emptyEnclosureEntities.MapEntitiesToContainers();

        return new GetEnclosuresContainer(
            Count: containers.Count,
            Enclosures: containers
        );
    }

    public async Task<GetEnclosuresContainer> GetFullEnclosures(CancellationToken cancellationToken)
    {
        var entities = await _enclosureRepository.GetAllEnclosures(cancellationToken);

        var emptyEnclosureEntities = entities.Where(e => e.CurrentCapacity == e.MaximumCapacity).ToList();

        IReadOnlyList<EnclosureModelContainer> containers = emptyEnclosureEntities.MapEntitiesToContainers();

        return new GetEnclosuresContainer(
            Count: containers.Count,
            Enclosures: containers
        );
    }
}
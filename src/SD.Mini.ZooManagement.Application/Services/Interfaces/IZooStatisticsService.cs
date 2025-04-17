using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Application.Containers.Statistics;

namespace SD.Mini.ZooManagement.Application.Services.Interfaces;

public interface IZooStatisticsService
{
    public Task<CountAnimalsTypeContainer> CountAnimalsTypes(CancellationToken cancellationToken);
    public Task<decimal> CalculateAnimalEnclosurePlacementRate(CancellationToken cancellationToken);
    public Task<GetEnclosuresContainer> GetEmptyEnclosures(CancellationToken cancellationToken);
    public Task<GetEnclosuresContainer> GetFullEnclosures(CancellationToken cancellationToken);
}
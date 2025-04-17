using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Services.Interfaces;

public interface IEnclosureService
{
    public Task<EntityId> AddNewEnclosure(EnclosureModel model, CancellationToken cancellationToken);
    public Task<EnclosureModelContainer> GetEnclosure(EntityId id, CancellationToken cancellationToken);
    public Task<IReadOnlyList<EnclosureModelContainer>> GetEnclosures(CancellationToken cancellationToken);
    public Task DeleteEnclosure(EntityId id, CancellationToken cancellationToken);
}
using SD.Mini.ZooManagement.Domain.Models.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Containers;

public record EnclosureModelContainer(
    EntityId Id,
    List<EntityId> AnimalsId,
    EnclosureModel EnclosureModel
);
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Containers;

public record AnimalModelContainer(
    EntityId AnimalId,
    EntityId? EnclosureId,
    AnimalModel AnimalModel
);
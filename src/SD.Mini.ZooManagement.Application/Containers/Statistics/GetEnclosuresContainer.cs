namespace SD.Mini.ZooManagement.Application.Containers.Statistics;

public record GetEnclosuresContainer(
    int Count,
    IReadOnlyList<EnclosureModelContainer> Enclosures
);
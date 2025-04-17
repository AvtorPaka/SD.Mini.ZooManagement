using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Application.Containers.Statistics;

public record CountAnimalsTypeContainer(
    int TotalCount,
    Dictionary<AnimalType, int> TypeCountDict
);
using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses.Statistics;

public record GetZooAnimalsCountResponse(
    int TotalCount,
    Dictionary<AnimalType, int> TypesCount
);
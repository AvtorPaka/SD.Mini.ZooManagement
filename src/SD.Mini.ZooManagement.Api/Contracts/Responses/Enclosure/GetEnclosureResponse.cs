using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses.Enclosure;

public record GetEnclosureResponse(
    string Id,
    IEnumerable<string> AnimalIds,
    EnclosureType Type,
    decimal Volume,
    uint MaximumCapacity,
    uint CurrentCapacity
);
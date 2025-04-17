using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Requests.Enclosure;

public record AddEnclosureRequest(
    EnclosureType Type,
    decimal Volume,
    uint MaximumCapacity
);
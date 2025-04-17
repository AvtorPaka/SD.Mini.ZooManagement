using SD.Mini.ZooManagement.Api.Contracts.Requests.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;

namespace SD.Mini.ZooManagement.Api.Mappers.Enclosure;

internal static class RequestMappers
{
    internal static EnclosureModel MapRequestToModel(this AddEnclosureRequest request)
    {
        return new EnclosureModel(
            Type: request.Type,
            Volume: request.Volume,
            MaximumCapacity: request.MaximumCapacity
        );
    }
}
using SD.Mini.ZooManagement.Api.Contracts.Responses.Enclosure;
using SD.Mini.ZooManagement.Application.Containers;

namespace SD.Mini.ZooManagement.Api.Mappers.Enclosure;

internal static class ResponseMappers
{
    internal static GetEnclosureResponse MapContainerToResponse(this EnclosureModelContainer container)
    {
        return new GetEnclosureResponse(
            Id: container.Id.ToString(),
            AnimalIds: container.AnimalsId.Select(i => i.ToString()),
            Type: container.EnclosureModel.Type,
            Volume: container.EnclosureModel.Volume,
            MaximumCapacity: container.EnclosureModel.MaximumCapacity,
            CurrentCapacity: container.EnclosureModel.CurrentCapacity
        );
    }

    internal static IEnumerable<GetEnclosureResponse> MapContainersToResponses(
        this IReadOnlyList<EnclosureModelContainer> containers)
    {
        return containers.Select(c => c.MapContainerToResponse());
    }
}
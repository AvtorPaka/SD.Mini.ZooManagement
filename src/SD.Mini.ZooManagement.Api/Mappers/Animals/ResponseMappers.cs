using SD.Mini.ZooManagement.Api.Contracts.Responses.Animals;
using SD.Mini.ZooManagement.Application.Containers;
using SD.Mini.ZooManagement.Domain.Models.Animal;

namespace SD.Mini.ZooManagement.Api.Mappers.Animals;

internal static class ResponseMappers
{
    internal static GetAnimalResponse MapContainerToResponse(this AnimalModelContainer container)
    {
        return new GetAnimalResponse(
            Id: container.AnimalId.ToString(),
            EnclosureId: container.EnclosureId?.ToString(),
            Type: container.AnimalModel.Type,
            Nickname: container.AnimalModel.Nickname,
            Birthday: container.AnimalModel.Birthday,
            Gender: container.AnimalModel.Gender,
            FavouriteFood: container.AnimalModel.FavouriteFood,
            HealthCondition: container.AnimalModel.HealthCondition
        );
    }

    internal static IEnumerable<GetAnimalResponse> MapContainersToResponse(
        this IReadOnlyList<AnimalModelContainer> containers)
    {
        return containers.Select(c => c.MapContainerToResponse());
    }
}
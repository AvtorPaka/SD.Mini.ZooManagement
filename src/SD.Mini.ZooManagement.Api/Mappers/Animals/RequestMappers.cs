using SD.Mini.ZooManagement.Api.Contracts.Requests.Animals;
using SD.Mini.ZooManagement.Domain.Models.Animal;

namespace SD.Mini.ZooManagement.Api.Mappers.Animals;

internal static class RequestMappers
{
    internal static AnimalModel MapRequestToModel(this AddAnimalRequest request)
    {
        return new AnimalModel(
            type: request.Type,
            nickname: NullOrTrim(request.Nickname),
            birthday: request.Birthday,
            gender: request.Gender,
            favouriteFood: NullOrTrim(request.FavouriteFood),
            healthCondition: request.HealthCondition
        );
    }


    private static string NullOrTrim(string? value)
    {
        return value == null ? "" : value.Trim();
    }
}
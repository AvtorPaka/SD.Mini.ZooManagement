using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Requests.Animals;

public record AddAnimalRequest(
    AnimalType Type,
    string Nickname,
    DateTime Birthday,
    AnimalGender Gender,
    string FavouriteFood,
    AnimalHealthCondition HealthCondition
);
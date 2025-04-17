using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;

namespace SD.Mini.ZooManagement.Api.Contracts.Responses.Animals;

public record GetAnimalResponse(
    string Id,
    string? EnclosureId,
    AnimalType Type,
    string Nickname,
    DateTime Birthday,
    AnimalGender Gender,
    string FavouriteFood,
    AnimalHealthCondition HealthCondition
);
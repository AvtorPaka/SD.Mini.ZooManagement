
namespace SD.Mini.ZooManagement.Api.Contracts.Requests.Animals;

public record ChangeAnimalEnclosureRequest(
    string AnimalId,
    string EnclosureId
);
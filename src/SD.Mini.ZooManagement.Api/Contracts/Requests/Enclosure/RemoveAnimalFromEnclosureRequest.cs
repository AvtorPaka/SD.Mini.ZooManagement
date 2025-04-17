namespace SD.Mini.ZooManagement.Api.Contracts.Requests.Enclosure;

public record RemoveAnimalFromEnclosureRequest(
    string EnclosureId,
    string AnimalId
);
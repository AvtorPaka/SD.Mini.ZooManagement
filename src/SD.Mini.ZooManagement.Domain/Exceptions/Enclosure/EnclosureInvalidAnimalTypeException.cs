using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;

public class EnclosureInvalidAnimalTypeException: DomainException
{
    public AnimalType AnimalType { get; }
    public EnclosureType InvalidEnclosureType { get; }
    
    public EnclosureInvalidAnimalTypeException(string? message, AnimalType animalType, EnclosureType invalidEnclosureType) : base(message)
    {
        AnimalType = animalType;
        InvalidEnclosureType = invalidEnclosureType;
    }
}
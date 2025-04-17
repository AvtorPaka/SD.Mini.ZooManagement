using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;

public class EnclosureInvalidAnimalTypeException: DomainException
{
    public EnclosureInvalidAnimalTypeException(string? message) : base(message)
    {
    }
}
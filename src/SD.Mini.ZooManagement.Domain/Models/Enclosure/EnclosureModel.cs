using SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.Enclosure.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Models.Enclosure;

public record EnclosureModel(
    EnclosureType Type,
    decimal Volume,
    uint MaximumCapacity
)
{
    public uint CurrentCapacity { get; private set; } = 0;

    public EnclosureModel(EnclosureType type, decimal volume, uint maximumCapacity, uint currentCapacity)
        : this(type, volume, maximumCapacity)
    {
        CurrentCapacity = currentCapacity;
    }

    // Analogs of removing and adding an animal to an enclosure, as required in the task to belong to model.
    // Since the domain model must be isolated, it cannot have direct communication
    // with repositories and its entities, which are responsible for the relationship
    // between animals and enclosures and have always been a significant and necessary part of Clean Architecture.
    // Rich Models - an approach created by retards -> (´﹃｀).
    public void IncreaseCurrentCapacity(AnimalModel animalModel)
    {
        ValidateAnimalType(animalModel.Type);

        if (CurrentCapacity == MaximumCapacity)
        {
            throw new EnclosureCapacityException("Enclosure capacity is out of range.");
        }

        CurrentCapacity++;
    }

    private void ValidateAnimalType(AnimalType animalType)
    {
        bool isValid = true;

        switch (animalType)
        {
            case AnimalType.Bird:
                if (Type != EnclosureType.Birdcage)
                {
                    isValid = false;
                }

                break;
            case AnimalType.Fish:
                if (Type != EnclosureType.Aquarium)
                {
                    isValid = false;
                }

                break;
            case AnimalType.HerbivoreMammal:
                if (Type != EnclosureType.HerbivoreCage)
                {
                    isValid = false;
                }

                break;
            case AnimalType.PredatorMammal:
                if (Type != EnclosureType.PredatorCage)
                {
                    isValid = false;
                }

                break;

            default:
                isValid = true;
                break;
        }

        if (!isValid)
        {
            throw new EnclosureInvalidAnimalTypeException("Invalid animal type", animalType, Type);
        }
    }

    public void DecreaseCurrentCapacity()
    {
        if (CurrentCapacity == 0)
        {
            throw new EnclosureCapacityException("Enclosure capacity is out of range.");
        }

        CurrentCapacity--;
    }
};
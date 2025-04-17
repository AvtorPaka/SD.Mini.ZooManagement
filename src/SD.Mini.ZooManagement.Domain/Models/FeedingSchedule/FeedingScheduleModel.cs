using SD.Mini.ZooManagement.Domain.Exceptions.FeedingSchedule;
using SD.Mini.ZooManagement.Domain.Models.Animal;
using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Models.FeedingSchedule;

public record FeedingScheduleModel(
    AnimalModel Animal,
    TimeOnly FeedingTime,
    FoodType FoodType
)
{
    public bool IsDone { get; private set; } = false;

    public FeedingScheduleModel(AnimalModel animal, TimeOnly feedingTime, FoodType foodType, bool isDone) :
        this(animal, feedingTime, foodType)
    {
        IsDone = isDone;
    }

    public void MarkDone()
    {
        IsDone = true;
    }

    public void ValidateFoodType()
    {
        bool isValid = true;

        switch (Animal.Type)
        {
            case AnimalType.HerbivoreMammal:
                if (FoodType != FoodType.HerbivoreFood)
                {
                    isValid = false;
                }

                break;
            case AnimalType.PredatorMammal:
                if (FoodType != FoodType.PredatorFood)
                {
                    isValid = false;
                }

                break;
            case AnimalType.Bird:
                if (FoodType != FoodType.BirdFood)
                {
                    isValid = false;
                }

                break;
            case AnimalType.Fish:
                if (FoodType != FoodType.FishFood)
                {
                    isValid = false;
                }

                break;
            default:
                isValid = false;
                break;
        }

        if (!isValid)
        {
            throw new FeedingScheduleInvalidFoodTypeException(
                message: "Invalid food type",
                animalType: Animal.Type,
                invalidFoodType: FoodType);
        }
    }
}
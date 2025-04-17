using SD.Mini.ZooManagement.Domain.Models.Animal.Value.Enums;
using SD.Mini.ZooManagement.Domain.Models.FeedingSchedule.Value.Enums;

namespace SD.Mini.ZooManagement.Domain.Exceptions.FeedingSchedule;

public class FeedingScheduleInvalidFoodTypeException: DomainException
{
    public AnimalType AnimalType { get; }
    public FoodType InvalidFoodType { get; }
    public FeedingScheduleInvalidFoodTypeException(string? message, AnimalType animalType, FoodType invalidFoodType) : base(message)
    {
        AnimalType = animalType;
        InvalidFoodType = invalidFoodType;
    }
}
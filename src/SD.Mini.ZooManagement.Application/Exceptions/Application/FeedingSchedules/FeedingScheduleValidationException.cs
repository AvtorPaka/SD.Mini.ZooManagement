using SD.Mini.ZooManagement.Domain.Exceptions.FeedingSchedule;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.FeedingSchedules;

public class FeedingScheduleValidationException : ApplicationException
{
    public FeedingScheduleInvalidFoodTypeException FoodTypeException { get; }
    
    public FeedingScheduleValidationException(string? message,
        FeedingScheduleInvalidFoodTypeException innerException) : base(message, innerException)
    {
        FoodTypeException = innerException;
    }
}
namespace SD.Mini.ZooManagement.Domain.Exceptions.FeedingSchedule;

public class FeedingScheduleInvalidFoodTypeException: DomainException
{
    public FeedingScheduleInvalidFoodTypeException(string? message) : base(message)
    {
    }
}
using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.FeedingSchedules;

public class FeedingScheduleNotFoundException: ApplicationException
{
    public EntityNotFoundException NotFoundException { get; }
    
    public FeedingScheduleNotFoundException(string? message, EntityNotFoundException innerException) : base(message, innerException)
    {
        NotFoundException = innerException;
    }
}
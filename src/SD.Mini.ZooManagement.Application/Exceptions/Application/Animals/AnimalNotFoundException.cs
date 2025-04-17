using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;

public class AnimalNotFoundException: ApplicationException
{
    public EntityNotFoundException NotFoundException { get; }
    
    public AnimalNotFoundException(string? message, EntityNotFoundException innerException) : base(message, innerException)
    {
        NotFoundException = innerException;
    }
}
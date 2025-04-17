namespace SD.Mini.ZooManagement.Domain.Exceptions.Animal;

public class AnimalAlreadyHealthyException: DomainException
{
    public AnimalAlreadyHealthyException(string? message) : base(message)
    {
    }
    
}
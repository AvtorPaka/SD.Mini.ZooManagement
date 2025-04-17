using SD.Mini.ZooManagement.Domain.Exceptions.Animal;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.Animals;

public class AnimalHealDuplicateException: ApplicationException
{
    public AnimalHealDuplicateException(string? message, AnimalAlreadyHealthyException? innerException) : base(message, innerException)
    {
    }
}
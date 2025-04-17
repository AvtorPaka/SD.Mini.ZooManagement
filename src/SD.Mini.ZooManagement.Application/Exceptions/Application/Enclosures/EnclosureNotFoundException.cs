using SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;

public class EnclosureNotFoundException: ApplicationException
{
    public EntityNotFoundException NotFoundException { get; }
    
    public EnclosureNotFoundException(string? message, EntityNotFoundException innerException) : base(message, innerException)
    {
        NotFoundException = innerException;
    }
}
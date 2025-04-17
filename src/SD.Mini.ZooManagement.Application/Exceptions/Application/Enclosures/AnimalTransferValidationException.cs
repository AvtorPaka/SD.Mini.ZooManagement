namespace SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;

public class AnimalTransferValidationException: ApplicationException
{
    public AnimalTransferValidationException(string? message) : base(message)
    {
    }
}
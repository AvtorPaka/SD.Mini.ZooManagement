namespace SD.Mini.ZooManagement.Application.Exceptions.Application;

public class ApplicationException: Exception
{
    protected ApplicationException(string? message) : base(message)
    {
    }

    protected ApplicationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
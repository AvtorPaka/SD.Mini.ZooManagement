using FluentValidation;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application;

public class ApplicationValidationException: ApplicationException
{
    public ValidationException ValidationException { get; init; }

    public ApplicationValidationException(string? message, ValidationException innerException) : base(message, innerException)
    {
        ValidationException = innerException;
    }
}
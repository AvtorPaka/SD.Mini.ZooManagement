using SD.Mini.ZooManagement.Domain.Exceptions.Enclosure;

namespace SD.Mini.ZooManagement.Application.Exceptions.Application.Enclosures;

public class EnclosureCapacityConflictException: ApplicationException
{
    public bool IsOverfill { get; }
    
    public EnclosureCapacityConflictException(string? message, bool isOverfill, EnclosureCapacityException? innerException) : base(message, innerException)
    {
        IsOverfill = isOverfill;
    }
}
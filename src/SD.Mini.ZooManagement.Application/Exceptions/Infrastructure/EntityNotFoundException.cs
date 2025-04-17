using SD.Mini.ZooManagement.Domain.Models.Val;

namespace SD.Mini.ZooManagement.Application.Exceptions.Infrastructure;

public class EntityNotFoundException: InfrastructureException
{
    public EntityId InvalidId { get; }

    public EntityNotFoundException(string? message, EntityId invalidId) : base(message)
    {
        InvalidId = invalidId;
    }
}
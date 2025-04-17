namespace SD.Mini.ZooManagement.Domain.Models.Val;

public class EntityId : IEquatable<EntityId>
{
    private string Id { get;}

    public EntityId()
    {
        Id = Guid.NewGuid().ToString();
    }

    public EntityId(string? id)
    {
        Id = id == null ? "" : id.Trim();
    }
    
    public override string ToString()
    {
        return Id;
    }

    public static bool operator ==(EntityId? left, EntityId? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Id == right.Id;
    }

    public static bool operator !=(EntityId? left, EntityId? right)
    {
        return !(left == right);
    }

    public bool Equals(EntityId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((EntityId)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
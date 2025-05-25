namespace SharedKernel.Primitives;
public abstract class Entity(Guid id) : IEquatable<Entity>
{
    public Guid Id { get; private set; } = id;
    public bool Equals(Entity? other)
    {
        if(other is null) return false;

        if(ReferenceEquals(this, other)) return true;

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

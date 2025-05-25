namespace Domain.Members.ValueObjects;

public sealed record FamilySize
{
    public FamilySize(int size)
    {
        Size = size;
    }
    public int Size { get; private set; }
}

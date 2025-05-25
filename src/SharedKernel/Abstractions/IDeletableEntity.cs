namespace SharedKernel.Abstractions;

public interface IDeletableEntity
{
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
}

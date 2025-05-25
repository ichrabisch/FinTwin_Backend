using Domain.Common.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;

public class Merchant : Entity, IDeletableEntity, IAuditableEntity
{
    protected Merchant() : base(Guid.NewGuid()) { }

    public Merchant(Guid id, string name, Address address, string phoneNumber)
        : base(id)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; private set; }
    public Address Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsDeleted {get; private set;}
    public DateTime? DeletedAt { get; private set;}
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public static Merchant Create(string name, Address address, string phoneNumber)
    {
        var merchant = new Merchant(Guid.NewGuid(), name, address, phoneNumber);
        return merchant;
    }
}
using Domain.Accounts.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;
public class Receipt : Entity, IDeletableEntity, IAuditableEntity
{
    protected Receipt()  : base(Guid.NewGuid())
    {
    }
    private Receipt(Guid id, Guid merchantId, PaymentMethodInfo paymentMethod) : base(id)
    {
        MerchantId = merchantId;
        PaymentMethod = paymentMethod;
        Items = new List<ReceiptItem>();
    }
    public Guid TransactionId { get; private set; }
    public Guid MerchantId { get; private set; }
    public Transaction Transaction { get; private set; }
    public Merchant Merchant { get; private set; }
    public List<ReceiptItem> Items { get; private set; }
    public PaymentMethodInfo PaymentMethod { get; private set; }
    public decimal Total { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime CreatedAt { get; private set;}
    public DateTime? UpdatedAt { get; private set; }

    public void AddReceiptItem(ReceiptItem receiptItem)
    {
        Items.Add(receiptItem);
        CalculateTotal();
    }

    public void RemoveReceiptItem(ReceiptItem receiptItem)
    {
        Items.Remove(receiptItem);
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        Total = Items.Sum(item => item.TotalPrice);
    }

    public static Receipt Create(Guid id, Merchant merchant, PaymentMethodInfo paymentMethod)
    {
        var receipt = new Receipt(id, merchant.Id, paymentMethod);
        receipt.Merchant = merchant;
        return receipt;
    }

    public void SetMerchant(Merchant merchant)
    {
        Merchant = merchant;
    }
}
using Domain.Accounts.ValueObjects;
using Application.Accounts.Commands.CreateTransaction;

namespace WebAPI.V1.Accounts.Endpoints.Handlers
{
    public sealed record CreateTransactionHandlerRequest
    (
        decimal Amount,
        string Description,
        TransactionType TransactionType,
        ReceiptHandlerRequest? Receipt
    )
    {
        public CreateTransactionCommand ToCommand(Guid accountId) => new CreateTransactionCommand(
            accountId,
            Amount,
            Description,
            TransactionType,
            Receipt?.ToDto()
        );
    }

    public sealed record ReceiptHandlerRequest
    (
        DateTime PurchaseDate,
        decimal TotalAmount,
        decimal TaxAmount,
        decimal DiscountAmount,
        PaymentMethodHandlerRequest PaymentMethod,
        MerchantHandlerRequest Merchant,
        List<ItemHandlerRequest> Items
    )
    {
        public ReceiptDto ToDto() => new ReceiptDto(
            PurchaseDate,
            TotalAmount,
            PaymentMethod.ToDto(),
            Merchant.ToDto(),
            Items.ConvertAll(item => item.ToDto())
        );
    }

    public sealed record PaymentMethodHandlerRequest
    (
        PaymentMethod Type,
        string? Last4
    )
    {
        public PaymentMethodDto ToDto() => new PaymentMethodDto(Type, Last4);
    }

    public sealed record MerchantHandlerRequest
    (
        string Name,
        string Country,
        string City,
        string Street,
        string PhoneNumber
    )
    {
        public MerchantDto ToDto() => new MerchantDto(Name, Country, City, Street, PhoneNumber);
    }

    public sealed record ItemHandlerRequest
    (
        string ItemName,
        string ItemDescription,
        string Category,
        string GeneralCategory,
        string Unit,
        decimal Quantity,
        decimal UnitPrice,
        decimal TotalPrice,
        decimal TaxRate
    )
    {
        public ItemDto ToDto() => new ItemDto(ItemName, ItemDescription, Category, GeneralCategory, Unit, Quantity, UnitPrice, TotalPrice, TaxRate);
    }
}
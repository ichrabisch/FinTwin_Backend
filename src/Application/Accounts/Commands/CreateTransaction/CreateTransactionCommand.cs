using Domain.Accounts.ValueObjects;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.Commands.CreateTransaction;

public sealed record CreateTransactionCommand(
        Guid AccountId,
        decimal Amount,
        string Description, 
        TransactionType TransactionType, 
        ReceiptDto? Receipt
    ): ICommand<Result<Guid>>;

public sealed record ReceiptDto(
    DateTime PurchaseDate,
    decimal TotalAmount,
    PaymentMethodDto PaymentMethod,
    MerchantDto Merchant,
    List<ItemDto> Items
    );
public sealed record PaymentMethodDto(
        PaymentMethod Type,
        string? Last4
    );

public sealed record MerchantDto(
           string Name,
           string Country,
           string City,
           string Street,
           string PhoneNumber
       );

public sealed record ItemDto(
        string ItemName,
        string ItemDescription,
        string Category,
        string GeneralCategory,
        string Unit,
        decimal Quantity,
        decimal UnitPrice,
        decimal TotalPrice,
        decimal TaxRate
    );
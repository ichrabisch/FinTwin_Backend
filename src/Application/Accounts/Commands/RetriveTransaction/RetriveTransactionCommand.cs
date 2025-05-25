using Application.Accounts.Commands.CreateTransaction;
using Domain.Accounts.ValueObjects;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.Commands.RetriveTransaction;

public sealed record RetriveTransactionCommand(
        Guid AccountId,
        DateTime? StartDate,
        DateTime? EndDate,
        TransactionType? TransactionType
    ) : ICommand<Result<List<RetriveTransactionDto>>>;

public sealed record RetriveTransactionDto
{
    public RetriveTransactionDto
    (
        Guid id,
        Guid accountId,
        decimal amount,
        string description,
        TransactionType transactionType,
        DateTime createdAt,
        DateTime? updatedAt
    )
    {
        this.Id = id;
        this.AccountId = accountId;
        this.Amount = amount;
        this.Description = description;
        this.TransactionType = transactionType;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
    }
    public RetriveTransactionDto(
        Guid id,
        Guid accountId,
        decimal amount,
        string description,
        TransactionType transactionType,
        DateTime createdAt,
        DateTime? updatedAt,
        ReceiptDto? receipt
    )
    {
        this.Id = id;
        this.AccountId = accountId;
        this.Amount = amount;
        this.Description = description;
        this.TransactionType = transactionType;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
        this.Receipt = receipt;
    }
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    public ReceiptDto? Receipt { get; set; }
}
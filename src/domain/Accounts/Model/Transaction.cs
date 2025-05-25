using Domain.Accounts.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;
public class Transaction : Entity, IAuditableEntity
{
    protected Transaction() : base(Guid.NewGuid()) { }
    private Transaction(Guid id, decimal amount, string description, TransactionType transactionType, Receipt? receipt) : base(id)
    {
        Amount = amount;
        Description = description;
        TransactionType = transactionType;
        Receipt = receipt;
    }
    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public Receipt? Receipt { get; private set; }

    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public static Transaction CreateExpense(Guid id, Account account, Receipt receipt, string description)
    {
        var transaction = new Transaction(id, receipt.Total, description, TransactionType.Expense, receipt);
        transaction.SetAccount(account);
        return transaction;
    }

    public static Transaction CreateIncome(Guid id, Account account, decimal amount, string description)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than 0");
        var transaction =  new Transaction(id, amount, description, TransactionType.Income, null);
        transaction.SetAccount(account);
        return transaction;
    }

    private void SetAccount(Account account)
    {
        Account = account;
        AccountId = account.Id;
    }
}
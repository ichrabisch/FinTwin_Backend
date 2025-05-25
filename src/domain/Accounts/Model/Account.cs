using Domain.Accounts.ValueObjects;
using Domain.Members.Model;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.Accounts.Model;
public class Account : AggregateRoot, IDeletableEntity, IAuditableEntity
{
    private Account(Guid id, string accountName, bool isPersonal) : base(id)
    {
        AccountName = accountName;
        IsPersonal = isPersonal;
        Transactions = new List<Transaction>();
    }
    public Member? Member { get; private set; }
    public Guid MemberId { get; private set; }
    public string AccountName { get; private set; }
    public decimal Balance { get; private set; }
    public bool IsPersonal { get; private set; }
    
    public List<Transaction> Transactions { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public static Account Create(Guid id, string accountName, bool isPersonal, Member member)
    {
        ArgumentException.ThrowIfNullOrEmpty(accountName);
        var account =  new Account(id, accountName, isPersonal);
        account.SetMember(member);
        return account;
    }
    public void Update(string accountName, bool isPersonal)
    {
        AccountName = accountName;
        IsPersonal = isPersonal;
        UpdatedAt = DateTime.UtcNow;
    }
    private void SetMember(Member member)
    {
        Member = member;
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
        CalculateBalance();
    }

    public void CalculateBalance()
    {
        Balance = Transactions.Sum(t => t.Amount * (t.TransactionType == TransactionType.Income ? 1 : -1));
    }
}
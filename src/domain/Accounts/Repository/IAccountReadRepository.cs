using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;

namespace Domain.Accounts.Repository;

public interface IAccountReadRepository
{
    Task<List<Account>> GetAccountsByMemberId(Guid memberId, CancellationToken cancellationToken);
    Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken);
    Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken);
}
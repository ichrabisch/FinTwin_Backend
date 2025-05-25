using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using SharedKernel.Primitives;

namespace Domain.Accounts.Repository;

public interface IAccountRepository
{
    Task Create<T>(T entity, CancellationToken cancellationToken) where T : Entity;

    Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Account>> GetAccountsByMemberId(Guid memberId, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>>GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken);
    Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken);
    Task<Receipt> GetReceiptByTransactionId(Guid transactionId, CancellationToken cancellationToken);

    Task UpdateAsync(Account account, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Domain.Accounts.ValueObjects;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Infrastructure.Data.Accounts;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    private readonly IAccountReadRepository _readRepository;
    private readonly IAccountWriteRepository _writeRepository;

    public AccountRepository(IDbOperations context, IUnitOfWork unitOfWork,
        IAccountReadRepository readRepository, IAccountWriteRepository writeRepository)
        : base(context, unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public Task Create<T>(T entity, CancellationToken cancellationToken) where T : Entity
        => _writeRepository.Create(entity, cancellationToken);

    public Task<List<Account>> GetAccountsByMemberId(Guid memberId, CancellationToken cancellationToken)
        => _readRepository.GetAccountsByMemberId(memberId, cancellationToken);

    public Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
        => _readRepository.GetAccountByIdAsync(id, cancellationToken);

    public Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken)
        => _readRepository.GetTransactionsByAccountIdAsync(accountId, startDate, endDate, transactionType, cancellationToken);

    public Task UpdateAsync(Account account, CancellationToken cancellationToken)
        => _writeRepository.UpdateAsync(account, cancellationToken);

    public Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken)
        => _readRepository.GetReceiptByIdAsync(receiptId, cancellationToken);
    public async Task<Receipt?> GetReceiptByTransactionId(Guid transactionId, CancellationToken cancellationToken)
    {
        return await _context.Set<Receipt>()
            .Where(Receipt => Receipt.TransactionId == transactionId)
            .Include(ri => ri.Items).FirstOrDefaultAsync(cancellationToken);
    }
}
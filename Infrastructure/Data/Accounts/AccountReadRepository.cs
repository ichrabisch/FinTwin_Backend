using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Domain.Accounts.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Accounts;

public class AccountReadRepository : IAccountReadRepository
{
    private readonly IDbOperations _context;

    public AccountReadRepository(IDbOperations context)
    {
        _context = context;
    }

    public async Task<List<Account>> GetAccountsByMemberId(Guid memberId, CancellationToken cancellationToken)
    {
        return await _context.Set<Account>()
            .Where(a => a.MemberId == memberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Account> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _context.Set<Account>()
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (account == null)
            throw new Exception($"Account with id {id} not found.");

        account.CalculateBalance();
        return account;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(
        Guid accountId,
        DateTime? startDate,
        DateTime? endDate,
        TransactionType? transactionType,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Transaction>()
            .Where(t => t.AccountId == accountId);

        if (startDate.HasValue)
            query = query.Where(t => t.CreatedAt >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(t => t.CreatedAt <= endDate.Value);

        if (transactionType.HasValue)
            query = query.Where(t => t.TransactionType == transactionType.Value);

        return await query
            .Include(t => t.Receipt)
                .ThenInclude(r => r!.PaymentMethod)
            .Include(t => t.Receipt)
                .ThenInclude(r => r!.Merchant)
            .Include(t => t.Receipt)
                .ThenInclude(r => r!.Items)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<Receipt> GetReceiptByIdAsync(Guid receiptId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Infrastructure.Data.Accounts;

public class AccountWriteRepository : IAccountWriteRepository
{
    private readonly IDbOperations _context;
    private readonly IUnitOfWork _unitOfWork;

    public AccountWriteRepository(IDbOperations context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Create<T>(T entity, CancellationToken cancellationToken) where T : Entity
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
    {
        if (account == null) throw new ArgumentNullException(nameof(account));

        _context.Set<Account>().Entry(account).State = EntityState.Modified;

        await UpdateTransactions(account, cancellationToken);
        await UpdateMerchantsAndReceipts(account, cancellationToken);

        account.CalculateBalance();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task UpdateTransactions(Account account, CancellationToken cancellationToken)
    {
        var dbTransactions = await _context.Set<Transaction>()
            .Where(t => t.AccountId == account.Id)
            .ToListAsync(cancellationToken);

        foreach (var transaction in account.Transactions)
        {
            var dbTransaction = dbTransactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (dbTransaction == null)
                await _context.Set<Transaction>().AddAsync(transaction, cancellationToken);
            else
                _context.Set<Transaction>().Entry(dbTransaction).CurrentValues.SetValues(transaction);
        }

        foreach (var dbTransaction in dbTransactions)
        {
            if (!account.Transactions.Any(t => t.Id == dbTransaction.Id))
                _context.Set<Transaction>().Remove(dbTransaction);
        }
    }

    private async Task UpdateMerchantsAndReceipts(Account account, CancellationToken cancellationToken)
    {
        var receipts = account.Transactions
            .Select(t => t.Receipt)
            .Where(r => r != null)
            .ToList();

        var merchants = receipts
            .Select(r => r!.Merchant)
            .Where(m => m != null)
            .ToList();

        foreach (var merchant in merchants)
        {
            var dbMerchant = await _context.Set<Merchant>().FirstOrDefaultAsync(m => m.Id == merchant!.Id, cancellationToken);
            if (dbMerchant == null)
                await _context.Set<Merchant>().AddAsync(merchant!, cancellationToken);
            else
                _context.Set<Merchant>().Entry(dbMerchant).CurrentValues.SetValues(merchant);
        }

        foreach (var receipt in receipts)
        {
            var dbReceipt = await _context.Set<Receipt>().FirstOrDefaultAsync(r => r!.Id == receipt!.Id, cancellationToken);
            if (dbReceipt == null)
                await Create(receipt!, cancellationToken);
            else
                _context.Set<Receipt>().Entry(dbReceipt).CurrentValues.SetValues(receipt);
        }
    }
}
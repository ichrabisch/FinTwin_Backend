using Domain.Accounts.Model;
using SharedKernel.Primitives;

namespace Domain.Accounts.Repository;

public interface IAccountWriteRepository
{
    Task Create<T>(T entity, CancellationToken cancellationToken) where T : Entity;
    Task UpdateAsync(Account account, CancellationToken cancellationToken);
    Task UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken);
}
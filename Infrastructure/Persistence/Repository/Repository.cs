using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Infrastructure.Persistence.Repository;

public class Repository<TEntity>(
    IDbOperations context,
    IUnitOfWork unitOfWork
    ) : IRepository<TEntity> where TEntity : Entity
{
    protected readonly IDbOperations _context = context;
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.InsertAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct)
        => await _context.Set<TEntity>().ToListAsync(ct);


    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _context.GetByIdAsync<TEntity>(id, cancellationToken);

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

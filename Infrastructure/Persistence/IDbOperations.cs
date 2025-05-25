using Microsoft.EntityFrameworkCore;
using SharedKernel.Primitives;
using System.Linq.Expressions;

namespace Infrastructure.Persistence;

public interface IDbOperations
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task InsertRangeAsync<TEntity>(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : Entity;

    Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : Entity;

    void Remove<TEntity>(TEntity entity)
            where TEntity : Entity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;
}

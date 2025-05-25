using Microsoft.EntityFrameworkCore;
using SharedKernel.Primitives;
using System.Linq.Expressions;

namespace Infrastructure.Persistence;

public class DbOperations(ApplicationDbContext context) : IDbOperations
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    public Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default) where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity?> GetByIdAsync<TEntity>(Guid id, CancellationToken ct) where TEntity : Entity
        => id == Guid.Empty
            ? null
            : await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken: ct);


    public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : Entity
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task InsertRangeAsync<TEntity>(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    public void Remove<TEntity>(TEntity entity) where TEntity : Entity
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class => _context.Set<TEntity>();

    public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : Entity
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Entry(entity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Retrieve the entity that failed
            var entry = ex.Entries.Single();
            var databaseValues = await entry.GetDatabaseValuesAsync(cancellationToken);

            if (databaseValues == null)
            {
                // The entity has been deleted by another user
                throw new Exception($"{typeof(TEntity).Name} with id {entity.Id} no longer exists in the database.");
            }

            // Refresh the original values to bypass next concurrency check
            entry.OriginalValues.SetValues(databaseValues);

            // Retry the save operation
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

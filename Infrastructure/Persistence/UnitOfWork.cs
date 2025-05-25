using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Infrastructure.Persistence;

public class UnitOfWork (ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await UpdateAuditableEntities();
        await UpdateDeletableEntities();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private Task UpdateAuditableEntities()
    {
        var utcNow = DateTime.UtcNow;
        var auditableEntities = _context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entity in auditableEntities)
        {
            if (entity.State == EntityState.Added)
                entity.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = utcNow;
            else if (entity.State == EntityState.Modified)
                entity.Property(nameof(IAuditableEntity.UpdatedAt)).CurrentValue = utcNow;
        }

        return Task.CompletedTask;
    }

    private async Task UpdateDeletableEntities()
    {
        var utcNow = DateTime.UtcNow;
        var deletableEntities = _context.ChangeTracker.Entries<IDeletableEntity>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entity in deletableEntities)
        {
            entity.Property(nameof(IDeletableEntity.DeletedAt)).CurrentValue = utcNow;
            entity.Property(nameof(IDeletableEntity.IsDeleted)).CurrentValue = true;
            entity.State = EntityState.Modified;
            await UpdateDeletedEntityReferences(entity);
        }
    }

    private async Task UpdateDeletedEntityReferences(EntityEntry entityEntry)
    {
        var references = entityEntry.References
            .Where(r => r.TargetEntry != null && r.TargetEntry.State == EntityState.Deleted);

        foreach (var reference in references)
        {
            reference.TargetEntry!.State = EntityState.Unchanged;
            await UpdateDeletedEntityReferences(reference.TargetEntry);
        }
    }
}

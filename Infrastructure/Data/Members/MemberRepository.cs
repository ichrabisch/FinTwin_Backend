using Domain.Members.Model;
using Domain.Members.Repository;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Infrastructure.Data.Members;

public class MemberRepository(IDbOperations context, IUnitOfWork unitOfWork) : Repository<Member>(context, unitOfWork), IMemberRepository
{
    public async Task Create(Member member, CancellationToken ct)
    {
        await AddAsync(member, ct);
    }

    public async Task<Member> Retrive(Guid id, CancellationToken ct)
    {
        return await GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Member with id {id} not found.");
    }

    public async Task<Member?> Retrive(string email, CancellationToken ct)
    {
        return await _context.Set<Member>()
            .FirstOrDefaultAsync(m => m.Email == email, ct);
    }
}

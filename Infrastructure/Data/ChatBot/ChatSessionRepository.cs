using Domain.ChatBot.Model;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;

namespace Infrastructure.Data.ChatBot;

public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(IDbOperations context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatSession>()
            .AnyAsync(cs => cs.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ChatSession>> GetActiveSessionsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatSession>()
            .Where(cs => cs.EndTime == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ChatSession>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatSession>()
            .Where(cs => cs.AccountId == accountId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetSessionCountForAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatSession>()
            .CountAsync(cs => cs.AccountId == accountId, cancellationToken);
    }
}

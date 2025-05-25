using Domain.ChatBot.Model;

public interface IChatSessionRepository
{
    Task<ChatSession> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatSession>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatSession>> GetActiveSessionsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ChatSession chatSession, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChatSession chatSession, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> GetSessionCountForAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
}
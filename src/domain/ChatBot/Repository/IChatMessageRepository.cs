using Domain.ChatBot.Model;
using Domain.ChatBot.ValueObjects;


public interface IChatMessageRepository
{
    Task<ChatMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatMessage>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChatMessage>> GetBySenderTypeAsync(Guid sessionId, SenderType senderType, CancellationToken cancellationToken = default);
    Task AddAsync(ChatMessage chatMessage, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChatMessage chatMessage, CancellationToken cancellationToken = default);
    Task<int> GetMessageCountForSessionAsync(Guid sessionId, CancellationToken cancellationToken = default);
}
using Domain.ChatBot.Model;
using Domain.ChatBot.ValueObjects;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.ChatBot;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(IDbOperations context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }

    public async Task<IEnumerable<ChatMessage>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatMessage>()
            .Where(cm => cm.SessionId == sessionId)
            .OrderBy(cm => cm.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ChatMessage>> GetBySenderTypeAsync(Guid sessionId, SenderType senderType, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatMessage>()
            .Where(cm => cm.SessionId == sessionId && cm.SenderType == senderType)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetMessageCountForSessionAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<ChatMessage>()
            .CountAsync(cm => cm.SessionId == sessionId, cancellationToken);
    }
}
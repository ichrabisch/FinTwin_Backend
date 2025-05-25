using Domain.Accounts.Model;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.ChatBot.Model;

public class ChatSession : Entity, IAuditableEntity
{
    protected ChatSession() : base(Guid.NewGuid()) { }
    public Guid AccountId { get; private set; }
    
    public Account Account { get; private set; }
    public ICollection<ChatMessage> Messages { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public DateTime? EndTime { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public ChatSession(Guid accountId) : base(Guid.NewGuid())
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("Account ID cannot be empty", nameof(accountId));

        
        AccountId = accountId;
        CreatedAt = DateTime.UtcNow;
        Messages = new List<ChatMessage>();
    }

    public void AddMessage(ChatMessage message)
    {
        if (EndTime.HasValue)
            throw new InvalidOperationException("Cannot add message to an ended session.");

        Messages.Add(message);
        UpdatedAt = DateTime.UtcNow;
    }

    public void EndSession()
    {
        if (EndTime.HasValue)
            throw new InvalidOperationException("Session has already ended.");

        EndTime = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActive => !EndTime.HasValue;
}

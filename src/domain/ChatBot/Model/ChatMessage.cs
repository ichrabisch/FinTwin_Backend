using Domain.ChatBot.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;

namespace Domain.ChatBot.Model;

public class ChatMessage : Entity, IAuditableEntity
{
    public Guid SessionId { get; private set; }
    public SenderType SenderType { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ChatSession ChatSession { get; private set; }

    protected ChatMessage() : base(Guid.NewGuid()) { }

    public ChatMessage(Guid id, Guid sessionId, string message, SenderType senderType) : base(id)
    {
        if (sessionId == Guid.Empty)
            throw new ArgumentException("Session ID cannot be empty", nameof(sessionId));
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message cannot be empty", nameof(message));

        SessionId = sessionId;
        Message = message;
        SenderType = senderType;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateMessage(string newMessage)
    {
        if (string.IsNullOrWhiteSpace(newMessage))
            throw new ArgumentException("New message cannot be empty", nameof(newMessage));

        Message = newMessage;
        UpdatedAt = DateTime.UtcNow;
    }
}

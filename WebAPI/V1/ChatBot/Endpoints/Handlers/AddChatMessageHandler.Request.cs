using Application.ChatBot.Commands.AddChatMessageCommand;
using Domain.ChatBot.ValueObjects;

namespace WebAPI.V1.ChatBot.Endpoints.Handlers;

public sealed record AddChatMessageHandlerRequest(string Content, SenderType SenderType)
{
    public AddChatMessageCommand ToCommand(Guid sessionId) =>
        new(sessionId, Content, SenderType);
}

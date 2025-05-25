using Application.ChatBot.Commands.StartChatSession;

namespace WebAPI.V1.ChatBot.Endpoints.Handlers;

public sealed record StartChatSessionHandlerRequest()
{
    public StartChatSessionCommand ToCommand(Guid accountId) =>
        new(accountId);
}
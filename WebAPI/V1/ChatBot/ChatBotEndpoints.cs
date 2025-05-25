using WebAPI.V1.ChatBot.Endpoints.Handlers;

namespace WebAPI.V1.ChatBot;

internal static class ChatBotEndpointsMap
{
    public static RouteGroupBuilder MapChatBotEndpointsMap(this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("chatsessions/{accountId}", ChatBotEndpoints.StartChatSessionHandler);
        routeGroupBuilder.MapPost("chatsessions/{sessionId}/messages", ChatBotEndpoints.AddChatMessageHandler);
        routeGroupBuilder.MapGet("chatsessions/{sessionId}/messages", ChatBotEndpoints.GetChatMessagesHandler);
        return routeGroupBuilder;
    }
}

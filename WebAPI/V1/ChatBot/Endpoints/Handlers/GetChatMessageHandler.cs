using Application.ChatBot.Commands;
using Application.ChatBot.Commands.AddChatMessageCommand;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.ChatBot.Endpoints.Handlers;

internal static partial class ChatBotEndpoints
{
    public static async Task<IResult> GetChatMessagesHandler(
        string sessionId,
        ICommandPublisher publisher,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(sessionId, out var id))
        {
            return Results.BadRequest(new Error("Invalid session id format"));
        }

        var command = new GetChatMessagesCommand(id);
        Result<List<ChatMessageDto>> result = await publisher.Publish<Result<List<ChatMessageDto>>, GetChatMessagesCommand>(command, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value);
    }
}

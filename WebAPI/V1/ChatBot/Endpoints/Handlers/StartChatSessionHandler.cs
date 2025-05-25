using Application.ChatBot.Commands.StartChatSession;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.ChatBot.Endpoints.Handlers;

internal static partial class ChatBotEndpoints
{
    public static async Task<IResult> StartChatSessionHandler(
        string accountId,
        ICommandPublisher publisher,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(accountId, out var id))
        {
            return Results.BadRequest(new Error("Invalid account id format"));
        }
        StartChatSessionHandlerRequest request = new();
        Result<Guid> result = await publisher.Publish<Result<Guid>, StartChatSessionCommand>(request.ToCommand(id), cancellationToken);
        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }
        return Results.Ok(result.Value);
    }
}
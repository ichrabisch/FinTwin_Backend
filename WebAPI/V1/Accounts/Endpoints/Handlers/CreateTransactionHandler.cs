using Application.Accounts.Commands.CreateTransaction;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

internal static partial class AccountEndpoints
{
    public static async Task<IResult> CreateTransactionHandler(
            string accountId,
            CreateTransactionHandlerRequest request,
            ICommandPublisher publisher,
            CancellationToken cancellationToken
        )
    {
        if(!Guid.TryParse(accountId, out var id)) {
            return Results.BadRequest(new Error("Invalid account id format"));
        }

        Result<Guid> result = await publisher.Publish<Result<Guid>, CreateTransactionCommand>(request.ToCommand(id), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}

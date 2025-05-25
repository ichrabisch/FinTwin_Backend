using Application.Accounts.Commands.RetriveTransaction;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

internal static partial class AccountEndpoints
{
    public static async Task<IResult> RetriveTransactionHandler(
        string accountId,
        [AsParameters] RetriveTransactionHandlerRequest request,
        ICommandPublisher publisher,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(accountId, out var id))
        {
            return Results.BadRequest(new Error("Invalid account id format"));
        }
        var command = new RetriveTransactionCommand(id, request.StartDate, request.EndDate, request.TransactionType);

        var result = await publisher.Publish<Result<List<RetriveTransactionDto>>, RetriveTransactionCommand>(
            command,
            cancellationToken
        );

        if (result.IsFailed)
        {
            var errorMessages = result.Errors.Select(e => e.Message).ToList();
            return Results.BadRequest(new { Errors = errorMessages });
        }

        if (!result.Value.Any())
        {
            return Results.NotFound("No transactions found for the specified account.");
        }

        return Results.Ok(result.Value);
    }
}

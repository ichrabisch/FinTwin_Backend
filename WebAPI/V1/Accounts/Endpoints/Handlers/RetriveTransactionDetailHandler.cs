using Application.Accounts.Commands.CreateTransaction;
using Application.Accounts.Commands.RetriveTransactionDetail;
using Application.Accounts.UseCases;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers
{
    internal static partial class AccountEndpoints
    { 
        public static async Task<IResult> RetriveTransactionDetailHandler(
                string transactionId,
                ICommandPublisher publisher,
                CancellationToken cancellationToken
            )
        {
            if(!Guid.TryParse(transactionId, out var id)) 
            {
                return Results.BadRequest("Bad transaction id.");

            }
            RetriveTransactionDetailCommand command = new RetriveTransactionDetailCommand(id);
            var result = await publisher.Publish<Result<ReceiptDto>, RetriveTransactionDetailCommand>(command, cancellationToken);
            if (result.IsFailed)
                return Results.BadRequest(result.Errors.Select(e => e.Message));

            return Results.Ok(result.Value);
        }
    }
}

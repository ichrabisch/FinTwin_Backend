using Application.Accounts.Commands.RetriveGeneralCategories;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;
internal static partial class AccountEndpoints
{
    public static async Task<IResult> RetriveGeneralCategoriesHandler(
                   string accountId,
                   [AsParameters] RetriveGeneralCategoriesHandler request,
                   ICommandPublisher publisher,
                   CancellationToken cancellationToken
               )
    {
        if(!Guid.TryParse(accountId, out var id))
        {
            return Results.BadRequest(new Error("Invalid account id format"));
        }

        Result<List<RetriveGeneralCategoriesDto>> result = await publisher.Publish<Result<List<RetriveGeneralCategoriesDto>>, RetriveGeneralCategoriesCommand>(request.ToCommand(id), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value);
    }
}

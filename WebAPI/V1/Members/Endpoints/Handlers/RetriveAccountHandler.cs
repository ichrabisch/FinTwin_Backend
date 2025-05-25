using Application.Accounts.Commands;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Members.Endpoints.Handlers;
internal static partial class MemberEndpoints
{
    public static async Task<IResult> RetriveAccountHandler(
               string memberId,
               ICommandPublisher publisher,
               CancellationToken cancellationToken
           )
    {

        if (!Guid.TryParse(memberId, out var id))
        {
            return Results.BadRequest(new Error("Invalid member id format"));
        }

        var command = new RetriveAccountCommand(id);

        var result = await publisher.Publish<Result<List<RetriveAccountDto>>, RetriveAccountCommand>(
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
            return Results.NotFound("No accounts found for the specified member.");
        }

        return Results.Ok(result.Value);
    }
}

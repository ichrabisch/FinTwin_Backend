using Application.Accounts.Commands.CreateAccount;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Members.Endpoints.Handlers;

internal static partial class MemberEndpoints
{
    public static async Task<IResult> CreateAccountHandler(
        string memberId,
        CreateAccountHandlerRequest request,
        ICommandPublisher publisher,
        CancellationToken cancellationToken
    )
    {
        if (!Guid.TryParse(memberId, out var id))
        {
            return Results.BadRequest(new Error("Invalid member id format"));
        }
        Result<Guid> result = await publisher.Publish<Result<Guid>, CreateAccountCommand>(request.ToCommand(id), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}

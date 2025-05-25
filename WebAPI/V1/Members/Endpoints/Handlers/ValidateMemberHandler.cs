using Application.Members.Commands.ValidateMember;
using FluentResults;
using SharedKernel.Abstractions;

namespace WebAPI.V1.Members.Endpoints.Handlers;

internal static partial class MemberEndpoints
{
    public static async Task<IResult> ValidateMemberHandler(
                   ValidateMemberHandlerRequest request,
                   ICommandPublisher publisher,
                   CancellationToken cancellationToken
               )
    {
        Result<string> result = await publisher.Publish<Result<string>, ValidateMemberCommand>(request.ToCommand(), cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.Errors.Select(e => e.Message).ToList());
        }

        return Results.Ok(result.Value.ToString());
    }
}

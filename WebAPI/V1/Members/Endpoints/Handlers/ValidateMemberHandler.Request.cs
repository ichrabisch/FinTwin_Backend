using Application.Members.Commands.ValidateMember;

namespace WebAPI.V1.Members.Endpoints.Handlers;

public sealed record ValidateMemberHandlerRequest(
        string Email,
        string Password
    )
{
       public ValidateMemberCommand ToCommand() => new(Email, Password);
}
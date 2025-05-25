using Application.Accounts.Commands.CreateAccount;

namespace WebAPI.V1.Members.Endpoints.Handlers;

public sealed record CreateAccountHandlerRequest
    (
        string AccountName,
        bool IsPersonel
    )
{
    public CreateAccountCommand ToCommand(Guid memberId) => new CreateAccountCommand(
                memberId,
                AccountName,
                IsPersonel
           );
}

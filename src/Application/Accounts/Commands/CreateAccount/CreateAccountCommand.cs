using FluentResults;
using SharedKernel.Abstractions;


namespace Application.Accounts.Commands.CreateAccount;
    public sealed record CreateAccountCommand(
        Guid MemberId,
        string AccountName,
        bool IsPersonal  
    ) : ICommand<Result<Guid>>;


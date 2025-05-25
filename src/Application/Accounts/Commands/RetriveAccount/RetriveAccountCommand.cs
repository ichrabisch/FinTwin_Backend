using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.Commands;

public sealed record RetriveAccountCommand(
        Guid MemberId
    ) : ICommand<Result<List<RetriveAccountDto>>>;

public sealed record RetriveAccountDto(
        Guid AccountId,
        string AccountName,
        decimal Balance,
        bool IsPersonal,
        DateTime CreatedAt
    );

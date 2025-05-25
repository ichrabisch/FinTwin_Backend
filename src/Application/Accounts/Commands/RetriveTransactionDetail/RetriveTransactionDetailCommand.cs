using Application.Accounts.Commands.CreateTransaction;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.Commands.RetriveTransactionDetail;

public sealed record RetriveTransactionDetailCommand(
    Guid TransactionId
    ) : ICommand<Result<ReceiptDto>>;
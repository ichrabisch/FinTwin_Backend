using Domain.Accounts.ValueObjects;
using FluentValidation;

namespace Application.Accounts.Commands.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required");
        RuleFor(x => x.TransactionType).NotEmpty().WithMessage("TransactionType is required");
    }
}

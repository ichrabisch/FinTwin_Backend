using FluentValidation;

namespace Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty().WithMessage("Account name is required")
            .MaximumLength(200).WithMessage("Account name must not exceed 200 characters");

        RuleFor(x => x.IsPersonal)
            .NotNull().WithMessage("Is personal is required");

        RuleFor(x => x.MemberId)
            .NotEmpty().WithMessage("Member id is required");
    }
}

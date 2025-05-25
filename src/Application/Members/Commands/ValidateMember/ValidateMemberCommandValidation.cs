using FluentValidation;

namespace Application.Members.Commands.ValidateMember;

public class ValidateMemberCommandValidator : AbstractValidator<ValidateMemberCommand>
{
    public ValidateMemberCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}


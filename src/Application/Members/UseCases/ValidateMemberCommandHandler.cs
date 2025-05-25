using Application.Common.Abstractions;
using Application.Members.Commands.ValidateMember;
using Domain.Members.Repository;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;

namespace Application.Members.UseCases;

public class ValidateMemberCommandHandler : ICommandHandler<ValidateMemberCommand, Result<string>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IValidator<ValidateMemberCommand> _validator;

    public ValidateMemberCommandHandler(IMemberRepository memberRepository, IJwtProvider jwtProvider,IValidator<ValidateMemberCommand> validator)
    {
        _memberRepository = memberRepository;
        _jwtProvider = jwtProvider;
        _validator = validator;
    }
    public async Task<Result<string>> Handle(ValidateMemberCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result.Fail<string>(validationResult.Errors.Select(e => new Error(e.ErrorMessage)));
        }

        var member = await _memberRepository.Retrive(command.Email, cancellationToken);

        if (member is null)
        {
            return Result.Fail<string>("Member not found");
        }

        if (member.Password != command.Password)
        {
            return Result.Fail<string>("Invalid password");
        }

        string token = _jwtProvider.Generate(member);

        return Result.Ok(token);
    }
}

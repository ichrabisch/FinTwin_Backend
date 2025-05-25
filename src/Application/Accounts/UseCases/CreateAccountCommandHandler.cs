using Application.Accounts.Commands.CreateAccount;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Accounts.Repository;
using Domain.Members.Repository;
using FluentResults;
using FluentValidation;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

internal class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Result<Guid>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IValidator<CreateAccountCommand> _validator;
    private readonly IMapper _mapper;
    private readonly IMemberRepository _memberRepository;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IValidator<CreateAccountCommand> validator,
        IMapper mapper,
        IMemberRepository memberRepository
    )
    {
        _accountRepository = accountRepository;
        _validator = validator;
        _mapper = mapper;
        _memberRepository = memberRepository;
    }
    public async Task<Result<Guid>> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.Retrive(command.MemberId, cancellationToken);
        if (member is null)
        {
            return Result.Fail<Guid>("Member not found");
        }

        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result.Fail<Guid>(validationResult.Errors.Select(e => new Error(e.ErrorMessage)));
        }

        var account = _mapper.Map<Account>(command, opt => opt.Items["Member"] = member);

        await _accountRepository.Create(account, cancellationToken);

        return Result.Ok(account.Id);
    }
}

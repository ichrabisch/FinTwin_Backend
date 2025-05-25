using Application.Accounts.Commands;
using AutoMapper;
using Domain.Accounts.Repository;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

internal class RetriveAccountCommandHandler : ICommandHandler<RetriveAccountCommand, Result<List<RetriveAccountDto>>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public RetriveAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RetriveAccountDto>>> Handle(RetriveAccountCommand command, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAccountsByMemberId(command.MemberId, cancellationToken);
        if (accounts == null)
        {
            return Result.Fail<List<RetriveAccountDto>>("No accounts found for this member.");
        }
        var accountDtos = _mapper.Map<List<RetriveAccountDto>>(accounts);
        return Result.Ok(accountDtos);
        
    }
}

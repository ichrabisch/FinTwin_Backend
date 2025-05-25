using Application.Accounts.Commands.RetriveTransaction;
using AutoMapper;
using Domain.Accounts.Repository;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

public class RetriveTransactionCommandHandler : ICommandHandler<RetriveTransactionCommand, Result<List<RetriveTransactionDto>>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public RetriveTransactionCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RetriveTransactionDto>>> Handle(RetriveTransactionCommand command, CancellationToken cancellationToken)
    {
        var transactions = await _accountRepository.GetTransactionsByAccountIdAsync(
                                                    command.AccountId,
                                                    command.StartDate,
                                                    command.EndDate,
                                                    command.TransactionType,
                                                    cancellationToken
                                                    );

        var transactionDtos = transactions?.Any() == true
            ? _mapper.Map<List<RetriveTransactionDto>>(transactions)
            : new List<RetriveTransactionDto>();

        return Result.Ok(transactionDtos);
    }
}
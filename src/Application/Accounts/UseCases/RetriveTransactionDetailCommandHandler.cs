
using Application.Accounts.Commands.CreateTransaction;
using Application.Accounts.Commands.RetriveTransactionDetail;
using AutoMapper;
using Domain.Accounts.Repository;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

public class RetriveTransactionDetailCommandHandler : ICommandHandler<RetriveTransactionDetailCommand, Result<ReceiptDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public RetriveTransactionDetailCommandHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;

        _mapper = mapper;
    }
    public async Task<Result<ReceiptDto>> Handle(RetriveTransactionDetailCommand command, CancellationToken cancellationToken)
    {
        var receipt = await _accountRepository.GetReceiptByTransactionId(command.TransactionId,cancellationToken);
        if (receipt == null)
        {
            return Result.Fail<ReceiptDto>("Receipt not found :P");
        }
        var receiptDto = _mapper.Map<ReceiptDto>(receipt);
        return Result.Ok(receiptDto);
    }
}

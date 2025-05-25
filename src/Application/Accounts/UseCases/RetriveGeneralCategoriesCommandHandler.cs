using Application.Accounts.Commands.RetriveGeneralCategories;
using AutoMapper;
using Domain.Accounts.Repository;
using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.UseCases;

public class RetriveGeneralCategoriesCommandHandler : ICommandHandler<RetriveGeneralCategoriesCommand, Result<List<RetriveGeneralCategoriesDto>>>
{
    private readonly IAccountRepository _accountRepository;
    
    public RetriveGeneralCategoriesCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<Result<List<RetriveGeneralCategoriesDto>>> Handle(RetriveGeneralCategoriesCommand command, CancellationToken cancellationToken)
    {
        var transactions = await _accountRepository.GetTransactionsByAccountIdAsync(
                                                    command.AccountId,
                                                    command.StartDate,
                                                    command.EndDate,
                                                    null,
                                                    cancellationToken);

        var receipts = transactions.Select(t => t.Receipt).ToList();
        Dictionary<string, decimal> categories = new Dictionary<string, decimal>();
        foreach(var reciept in receipts)
        {
            if(reciept == null)
            {
                continue;
            }
            foreach(var item in reciept!.Items)
            {
                categories[item.GeneralCategory] = categories.ContainsKey(item.GeneralCategory) ? categories[item.GeneralCategory] + item.TotalPrice : item.TotalPrice;
            }
        }
        var categoriesDtos = categories.Select(c => new RetriveGeneralCategoriesDto(c.Key, c.Value)).ToList();
        return Result.Ok(categoriesDtos);
    }
}

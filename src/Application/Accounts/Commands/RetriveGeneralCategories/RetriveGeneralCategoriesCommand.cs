using FluentResults;
using SharedKernel.Abstractions;

namespace Application.Accounts.Commands.RetriveGeneralCategories;

public sealed record RetriveGeneralCategoriesCommand(
        Guid AccountId,
        DateTime? StartDate,
        DateTime? EndDate
    ) : ICommand<Result<List<RetriveGeneralCategoriesDto>>>;

public sealed record RetriveGeneralCategoriesDto(string Category, decimal TotalAmount);
using Application.Accounts.Commands.RetriveGeneralCategories;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

public sealed record RetriveGeneralCategoriesHandler
(
    DateTime? StartDate,
    DateTime? EndDate
)
{
    public RetriveGeneralCategoriesCommand ToCommand(Guid accountId) =>
        new(accountId, StartDate, EndDate);
}

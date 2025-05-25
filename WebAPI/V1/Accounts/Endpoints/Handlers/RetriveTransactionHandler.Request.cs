using Domain.Accounts.ValueObjects;

namespace WebAPI.V1.Accounts.Endpoints.Handlers;

public sealed record RetriveTransactionHandlerRequest(
        DateTime? StartDate,
        DateTime? EndDate,
        TransactionType? TransactionType
    );

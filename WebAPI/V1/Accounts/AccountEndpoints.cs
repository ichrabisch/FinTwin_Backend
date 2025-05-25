using WebAPI.V1.Accounts.Endpoints.Handlers;

namespace WebAPI.V1.Accounts;

internal static class AccountEndpointsMap
{
    public static RouteGroupBuilder MapAccountEndpoints(this RouteGroupBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost("account/{accountId}/transaction", AccountEndpoints.CreateTransactionHandler);
        routeGroupBuilder.MapGet("account/{accountId}/transaction", AccountEndpoints.RetriveTransactionHandler);
        routeGroupBuilder.MapGet("account/{accountId}/general-categories", AccountEndpoints.RetriveGeneralCategoriesHandler);


        routeGroupBuilder.MapGet("transaction/{transactionId}", AccountEndpoints.RetriveTransactionDetailHandler);
        return routeGroupBuilder;
    }
}

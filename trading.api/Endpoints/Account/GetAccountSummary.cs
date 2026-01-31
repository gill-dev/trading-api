using trading.application.Abstractions.Messaging;
using trading.application.Features.Account.GetAccountSummary;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Account;

public static class GetAccountSummary
{
    public const string Name = "GetAccountSummary";

    public static IEndpointRouteBuilder MapGetAccountSummary(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ApiEndpoints.Account.Summary, HandleAsync)
            .WithName(Name)
            .Produces<AccountResponse>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);
        
        return endpoints;
    }

    public static async Task<IResult> HandleAsync(IQueryHandler<GetAccountSummaryQuery, AccountResponse> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetAccountSummaryQuery();
        
        var result = await handler.Handle(query, cancellationToken);
        
        if(result.IsError)
            Results.NotFound(result.Error);
        
        return Results.Ok(result.Value);
    }

}
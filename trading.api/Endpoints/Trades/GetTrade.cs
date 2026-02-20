using trading.application.Abstractions.Messaging;
using trading.application.Features.Trades.GetTrade;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Trades;

public static class GetTrade
{
    public const string Name = "GetTrade";

    public static IEndpointRouteBuilder MapGetTrade(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ApiEndpoints.Trades.GetTrade, HandleAsync)
            .WithName(Name)
            .Produces<TradeResponse>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        IQueryHandler<GetTradeQuery, TradeResponse> handler,
        string tradeId,
        CancellationToken cancellationToken)
    {
        var query = new GetTradeQuery(tradeId);
        var result = await handler.Handle(query, cancellationToken);

        if (result.IsError)
            return Results.NotFound(result.Error);

        return Results.Ok(result.Value);
    }
}

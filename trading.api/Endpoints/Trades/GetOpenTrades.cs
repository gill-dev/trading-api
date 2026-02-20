using trading.application.Abstractions.Messaging;
using trading.application.Features.Trades.GetOpenTrades;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Trades;

public static class GetOpenTrades
{
    public const string Name = "GetOpenTrades";

    public static IEndpointRouteBuilder MapGetOpenTrades(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ApiEndpoints.Trades.GetTrades, HandleAsync)
            .WithName(Name)
            .Produces<TradeResponse[]>(statusCode: StatusCodes.Status200OK);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        IQueryHandler<GetOpenTradesQuery, TradeResponse[]> handler,
        CancellationToken cancellationToken)
    {
        var query = new GetOpenTradesQuery();
        var result = await handler.Handle(query, cancellationToken);

        if (result.IsError)
            return Results.NotFound(result.Error);

        return Results.Ok(result.Value);
    }
}

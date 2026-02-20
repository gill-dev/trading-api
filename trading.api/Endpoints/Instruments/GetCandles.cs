using trading.application.Abstractions.Messaging;
using trading.application.Features.Instruments.GetCandles;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Instruments;

public static class GetCandles
{
    public const string Name = "GetCandles";

    public static IEndpointRouteBuilder MapGetCandles(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ApiEndpoints.Instrument.GetCandles, HandleAsync)
            .WithName(Name)
            .Produces<CandleResponse>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        IQueryHandler<GetCandlesQuery, CandleResponse> handler,
        string instrument,
        string? granularity,
        int? count,
        CancellationToken cancellationToken)
    {
        var query = new GetCandlesQuery(instrument, granularity, count ?? 500);
        var result = await handler.Handle(query, cancellationToken);

        if (result.IsError)
            return Results.NotFound(result.Error);

        return Results.Ok(result.Value);
    }
}

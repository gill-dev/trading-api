using trading.application.Abstractions.Messaging;
using trading.application.Features.Instruments.GetInstruments;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Instruments;

public static class GetInstruments
{
    public const string Name = "GetInstruments";

    public static IEndpointRouteBuilder MapGetInstruments(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(ApiEndpoints.Instrument.GetInstruments, HandleAsync)
            .WithName(Name)
            .Produces<InstrumentResponse[]>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        IQueryHandler<GetInstrumentsQuery, InstrumentResponse[]> handler,
        string? instruments,
        CancellationToken cancellationToken)
    {
        var query = new GetInstrumentsQuery(instruments);
        var result = await handler.Handle(query, cancellationToken);

        if (result.IsError)
            return Results.NotFound(result.Error);

        return Results.Ok(result.Value);
    }
}

using trading.application.Abstractions.Messaging;
using trading.application.Features.Trades.CloseTrade;

namespace trading.api.Endpoints.Trades;

public static class CloseTrade
{
    public const string Name = "CloseTrade";

    public static IEndpointRouteBuilder MapCloseTrade(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut(ApiEndpoints.Trades.CloseTrade, HandleAsync)
            .WithName(Name)
            .Produces(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status400BadRequest);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        ICommandHandler<CloseTradeCommand> handler,
        string tradeId,
        CancellationToken cancellationToken)
    {
        var command = new CloseTradeCommand(tradeId);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsError)
            return Results.BadRequest(result.Error);

        return Results.Ok();
    }
}

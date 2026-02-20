using trading.application.Abstractions.Messaging;
using trading.application.Features.Orders.PlaceOrder;
using Trading.Contracts.Requests;

namespace trading.api.Endpoints.Orders;

public static class PlaceOrder
{
    public const string Name = "PlaceOrder";

    public static IEndpointRouteBuilder MapPlaceOrder(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(ApiEndpoints.Orders.PlaceOrder, HandleAsync)
            .WithName(Name)
            .Produces<OrderFilledResponse>(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status400BadRequest);

        return endpoints;
    }

    private static async Task<IResult> HandleAsync(
        ICommandHandler<PlaceOrderCommand, OrderFilledResponse> handler,
        PlaceOrderCommand command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsError)
            return Results.BadRequest(result.Error);

        return Results.Ok(result.Value);
    }
}

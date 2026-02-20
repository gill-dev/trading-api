using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;
using trading.domain.Models;

namespace trading.application.Features.Orders.PlaceOrder;

internal sealed class PlaceOrderHandler : ICommandHandler<PlaceOrderCommand, OrderFilledResponse>
{
    private readonly IOandaApiService _oandaApiService;

    public PlaceOrderHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<OrderFilledResponse>> Handle(
        PlaceOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = new Order(
            instrument: command.Instrument,
            units: command.Units,
            tradeUnitsPrecision: command.TradeUnitsPrecision,
            displayPrecision: command.DisplayPrecision,
            isSell: command.IsSell,
            stopLoss: command.StopLoss,
            takeProfit: command.TakeProfit,
            trailingStop: command.TrailingStop);

        var response = await _oandaApiService.PlaceTradeAsync(order, cancellationToken);

        if (response is null)
            return Result.Failure<OrderFilledResponse>(OrderErrors.PlaceFailed);

        return Result.Success(response);
    }
}

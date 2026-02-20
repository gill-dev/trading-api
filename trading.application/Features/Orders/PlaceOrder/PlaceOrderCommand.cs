using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Orders.PlaceOrder;

public sealed record PlaceOrderCommand(
    string Instrument,
    double Units,
    bool IsSell,
    int TradeUnitsPrecision = 0,
    int DisplayPrecision = 5,
    double StopLoss = 0,
    double TakeProfit = 0,
    double TrailingStop = 0) : ICommand<OrderFilledResponse>;

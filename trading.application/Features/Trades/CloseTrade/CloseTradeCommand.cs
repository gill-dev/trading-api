using trading.application.Abstractions.Messaging;

namespace trading.application.Features.Trades.CloseTrade;

public sealed record CloseTradeCommand(string TradeId) : ICommand;

using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Trades.GetOpenTrades;

public sealed record GetOpenTradesQuery() : IQuery<TradeResponse[]>;

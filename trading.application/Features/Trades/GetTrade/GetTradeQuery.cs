using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Trades.GetTrade;

public sealed record GetTradeQuery(string TradeId) : IQuery<TradeResponse>;

using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Instruments.GetCandles;

public sealed record GetCandlesQuery(string Instrument, string? Granularity = null, int Count = 500) : IQuery<CandleResponse>;

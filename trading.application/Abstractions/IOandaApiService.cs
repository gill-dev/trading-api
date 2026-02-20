using Trading.Contracts.Requests;
using trading.domain.Models;

namespace trading.application.Abstractions;

public interface IOandaApiService
{
    Task<AccountResponse?> GetAccountSummaryAsync(CancellationToken cancellationToken = default);
    Task<CandleResponse?> GetCandlesAsync(string instrument, string? granularity = null, int count = 500, CancellationToken cancellationToken = default);
    Task<InstrumentResponse[]?> GetInstrumentsAsync(string? instruments = null, CancellationToken cancellationToken = default);
    Task<TradeResponse[]> GetOpenTradesAsync(CancellationToken cancellationToken = default);
    Task<TradeResponse?> GetTradeAsync(string tradeId, CancellationToken cancellationToken = default);
    Task<OrderFilledResponse?> PlaceTradeAsync(Order order, CancellationToken cancellationToken = default);
    Task<bool> CloseTradeAsync(string tradeId, CancellationToken cancellationToken = default);
}

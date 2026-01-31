using trading.application.Abstractions;
using trading.application.Features.Account.GetAccountSummary;
using Trading.Contracts.Requests;
using trading.domain.Models;
using trading.infrastructure.Http;

namespace trading.infrastructure.Services;

public sealed class OandaApiService : IOandaApiService
{
    private readonly ApiClientBase _apiClient;
    private readonly string _accountId;

    private const string DefaultGranularity = "H1";
    private const string DefaultPrice = "MBA";

    public OandaApiService(ApiClientBase apiClient, string accountId)
    {
        _apiClient = apiClient;
        _accountId = accountId;
    }

    public async Task<AccountResponse?> GetAccountSummaryAsync(CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/summary";
        return await _apiClient.GetAsync<AccountResponse>(endpoint, "account", cancellationToken);
    }
    public async Task<CandleResponse?> GetCandlesAsync(string instrument, string? granularity = null, int count = 500, CancellationToken cancellationToken = default)
    {
        var endpoint = BuildCandlesEndpoint(instrument, granularity ?? DefaultGranularity, count);
        return await _apiClient.GetAsync<CandleResponse>(endpoint, "candles", cancellationToken);

    }

    public async Task<InstrumentResponse[]?> GetInstrumentsAsync(string? instruments = null, CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/instruments";

        if (!string.IsNullOrEmpty(instruments))
            endpoint += $"?instruments={instruments}";

        return await _apiClient.GetAsync<InstrumentResponse[]>(endpoint, "instruments", cancellationToken);

    }

    public async Task<TradeResponse[]> GetOpenTradesAsync(CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/openTrades";
        return await _apiClient.GetAsync<TradeResponse[]>(endpoint, "trades",cancellationToken) ?? [];
    }

    public async Task<TradeResponse?> GetTradeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/trades/{tradeId}";
        return await _apiClient.GetAsync<TradeResponse>(endpoint, "trade",cancellationToken);
    }

    public async Task<OrderFilledResponse?> PlaceTradeAsync(Order order, CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/orders";
        var request = new OrderRequest(order);
        return await _apiClient.PostAsync<OrderFilledResponse>(endpoint, request, "orderFillTransaction", cancellationToken);
    }

    public async Task<bool> CloseTradeAsync(string tradeId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"accounts/{_accountId}/trades/{tradeId}/close";
        var response = await _apiClient.PutAsync<OrderFilledResponse>(endpoint, dataKey: "orderFillTransaction", cancellationToken: cancellationToken);
        return response is not null;
    }

    private static string BuildCandlesEndpoint(string instrument, string granularity, int count)
    {
        return $"instruments/{instrument}/candles?granularity={granularity}&price={DefaultPrice}&count={count}";
    }
}
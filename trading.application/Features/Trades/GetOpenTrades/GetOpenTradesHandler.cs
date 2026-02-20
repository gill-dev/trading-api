using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;

namespace trading.application.Features.Trades.GetOpenTrades;

internal sealed class GetOpenTradesHandler : IQueryHandler<GetOpenTradesQuery, TradeResponse[]>
{
    private readonly IOandaApiService _oandaApiService;

    public GetOpenTradesHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<TradeResponse[]>> Handle(
        GetOpenTradesQuery query,
        CancellationToken cancellationToken = default)
    {
        var response = await _oandaApiService.GetOpenTradesAsync(cancellationToken);
        return Result.Success(response);
    }
}

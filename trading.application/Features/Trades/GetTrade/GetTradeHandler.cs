using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;

namespace trading.application.Features.Trades.GetTrade;

internal sealed class GetTradeHandler : IQueryHandler<GetTradeQuery, TradeResponse>
{
    private readonly IOandaApiService _oandaApiService;

    public GetTradeHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<TradeResponse>> Handle(
        GetTradeQuery query,
        CancellationToken cancellationToken = default)
    {
        var response = await _oandaApiService.GetTradeAsync(query.TradeId, cancellationToken);

        if (response is null)
            return Result.Failure<TradeResponse>(TradeErrors.NotFound);

        return Result.Success(response);
    }
}

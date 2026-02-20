using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;

namespace trading.application.Features.Instruments.GetCandles;

internal sealed class GetCandlesHandler : IQueryHandler<GetCandlesQuery, CandleResponse>
{
    private readonly IOandaApiService _oandaApiService;

    public GetCandlesHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<CandleResponse>> Handle(
        GetCandlesQuery query,
        CancellationToken cancellationToken = default)
    {
        var response = await _oandaApiService.GetCandlesAsync(
            query.Instrument, query.Granularity, query.Count, cancellationToken);

        if (response is null)
            return Result.Failure<CandleResponse>(InstrumentErrors.CandlesNotFound);

        return Result.Success(response);
    }
}

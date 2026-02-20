using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;

namespace trading.application.Features.Instruments.GetInstruments;

internal sealed class GetInstrumentsHandler : IQueryHandler<GetInstrumentsQuery, InstrumentResponse[]>
{
    private readonly IOandaApiService _oandaApiService;

    public GetInstrumentsHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<InstrumentResponse[]>> Handle(
        GetInstrumentsQuery query,
        CancellationToken cancellationToken = default)
    {
        var response = await _oandaApiService.GetInstrumentsAsync(query.Instruments, cancellationToken);

        if (response is null)
            return Result.Failure<InstrumentResponse[]>(InstrumentErrors.NotFound);

        return Result.Success(response);
    }
}

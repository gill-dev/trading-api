using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using trading.domain;

namespace trading.application.Features.Trades.CloseTrade;

internal sealed class CloseTradeHandler : ICommandHandler<CloseTradeCommand>
{
    private readonly IOandaApiService _oandaApiService;

    public CloseTradeHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result> Handle(
        CloseTradeCommand command,
        CancellationToken cancellationToken = default)
    {
        var success = await _oandaApiService.CloseTradeAsync(command.TradeId, cancellationToken);

        if (!success)
            return Result.Failure(TradeErrors.CloseFailed);

        return Result.Success();
    }
}

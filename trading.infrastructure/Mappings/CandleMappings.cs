using Trading.Contracts.Requests;

namespace trading.infrastructure.Mappings;

internal static class CandleMappings
{
    public static CandleResponse[] ToCandles(this CandleData[] candles)
    {
        return candles
            .Where(c => c.Complete)
            .Select(c => new CandleResponse(c))
            .ToArray();
    }
    
    public static CandleResponse ToCandle(this CandleData candle) => new(candle);
}
using trading.domain;

namespace trading.application.Features.Instruments;

public class InstrumentErrors
{
    public static readonly Error NotFound = new("Instruments.NotFound", "Instruments not found");
    public static readonly Error CandlesNotFound = new("Candles.NotFound", "Candle data not found");
}

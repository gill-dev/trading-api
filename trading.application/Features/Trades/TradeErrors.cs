using trading.domain;

namespace trading.application.Features.Trades;

public class TradeErrors
{
    public static readonly Error NotFound = new("Trade.NotFound", "Trade not found");
    public static readonly Error CloseFailed = new("Trade.CloseFailed", "Failed to close trade");
}

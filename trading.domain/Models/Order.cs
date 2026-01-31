namespace trading.domain.Models;

public class Order
{
    public string Type { get; }
    public string Instrument { get; }
    public double Units { get; }
    public string TimeInForce { get; }
    public string PositionFill { get; }
    public StopLossOnFill? StopLossOnFill { get; }
    public TakeProfitOnFill? TakeProfitOnFill { get; }
    public TrailingStopLossOnFill? TrailingStopLossOnFill { get; }

    public Order(
        string instrument,
        double units,
        int tradeUnitsPrecision,
        int displayPrecision,
        bool isSell,
        double stopLoss = 0,
        double takeProfit = 0,
        double trailingStop = 0,
        string type = "MARKET",
        string timeInForce = "FOK",
        string positionFill = "DEFAULT")
    {
        Type = type;
        Instrument = instrument;
        Units = Math.Round(isSell ? -units : units, tradeUnitsPrecision);
        TimeInForce = timeInForce;
        PositionFill = positionFill;

        StopLossOnFill = stopLoss == 0 ? null : new StopLossOnFill(Math.Round(stopLoss, displayPrecision));
        TakeProfitOnFill = takeProfit == 0 ? null : new TakeProfitOnFill(Math.Round(takeProfit, displayPrecision));
        TrailingStopLossOnFill = trailingStop == 0 ? null : new TrailingStopLossOnFill(Math.Round(trailingStop, displayPrecision));
    }
}

public record StopLossOnFill(double Price);

public record TakeProfitOnFill(double Price);

public record TrailingStopLossOnFill(double Distance);
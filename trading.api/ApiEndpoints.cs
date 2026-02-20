namespace trading.api;

public static class ApiEndpoints
{
    private const string Api = "api";

    public static class Trades
    {
        private const string Base = $"{Api}/trades";

        public const string GetTrades = Base;
        public const string GetTrade = $"{Base}/{{tradeId}}";
        public const string CloseTrade = $"{Base}/{{tradeId}}/close";
    }

    public static class Account
    {
        private const string Base = $"{Api}/account";
        public const string Summary = Base;
    }

    public static class Instrument
    {
        private const string Base = $"{Api}/instruments";
        public const string GetInstruments = Base;
        public const string GetCandles = $"{Base}/{{instrument}}/candles";
    }

    public static class Orders
    {
        private const string Base = $"{Api}/orders";
        public const string PlaceOrder = Base;
    }
}

namespace trading.api;

public static class ApiEndpoints
{
    private const string Api = "api";

    public static class Trades
    {
        private const string Base = $"{Api}/trades";

        public const string GetTrades = $"{Base}/trades";
    }

    public static class Account
    {
        private const string Base = $"{Api}/account";
        public const string Summary = Base;
    }
}
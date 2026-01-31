namespace trading.infrastructure;

public sealed class OandaSettings
{
    public string BaseUrl { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
    public string AccountId { get; init; } = string.Empty;
}
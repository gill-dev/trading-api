using System.Text.Json.Serialization;

namespace Trading.Contracts.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GuaranteedStopLossOrderMode
{
    ALLOWED,
    DISABLED,
    REQUIRED
}
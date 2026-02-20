using trading.domain;

namespace trading.application.Features.Orders;

public class OrderErrors
{
    public static readonly Error PlaceFailed = new("Order.PlaceFailed", "Failed to place order");
}

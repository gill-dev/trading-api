using trading.api.Endpoints.Account;
using trading.api.Endpoints.Instruments;
using trading.api.Endpoints.Orders;
using trading.api.Endpoints.Trades;

namespace trading.api.Endpoints;

public static class EndpointMapper
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointGroup = endpoints.MapGroup("");

        endpointGroup
            .MapGetAccountSummary()
            .MapGetCandles()
            .MapGetInstruments()
            .MapGetOpenTrades()
            .MapCloseTrade()
            .MapPlaceOrder()
            .MapGetTrade();

        return endpoints;

    }
}
    

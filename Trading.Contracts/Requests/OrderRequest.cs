namespace Trading.Contracts.Requests;

public record OrderRequest
{
    public object Order { get; }

    public OrderRequest(object order)
    {
        Order = order;
    }

}
namespace Trading.Contracts.Requests;

public record AccountRequest
{
    public bool Download { get; init; }
}
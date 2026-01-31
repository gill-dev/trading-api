using trading.application.Abstractions;
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;
using trading.domain;

namespace trading.application.Features.Account.GetAccountSummary;

internal sealed class GetAccountSummaryHandler: IQueryHandler<GetAccountSummaryQuery, AccountResponse>
{
    private readonly IOandaApiService _oandaApiService;

    public GetAccountSummaryHandler(IOandaApiService oandaApiService)
    {
        _oandaApiService = oandaApiService;
    }

    public async Task<Result<AccountResponse>> Handle(
        GetAccountSummaryQuery query, 
        CancellationToken cancellationToken = default)
    {
        var response = await _oandaApiService.GetAccountSummaryAsync(cancellationToken);

        if (response is null)
            return Result.Failure<AccountResponse>(AccountErrors.NotFound);

        return Result.Success(response);
    }
}
using trading.application.Features.Account.GetAccountSummary;
using Trading.Contracts.Requests;

namespace trading.application.Abstractions;

public interface IOandaApiService
{
    Task<AccountResponse?> GetAccountSummaryAsync(CancellationToken cancellationToken = default);
    
}
using trading.application.Abstractions.Messaging;
using Trading.Contracts.Requests;

namespace trading.application.Features.Account.GetAccountSummary;

public sealed record GetAccountSummaryQuery() : IQuery<AccountResponse>;
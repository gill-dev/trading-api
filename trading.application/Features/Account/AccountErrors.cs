using trading.domain;

namespace trading.application.Features.Account;

public class AccountErrors
{
    public static readonly Error NotFound = new("Account.NotFound", "Account not found");
}
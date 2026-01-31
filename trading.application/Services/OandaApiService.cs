using Microsoft.Extensions.Logging;

namespace trading.application.Services;

public class OandaApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OandaApiService> _logger;
    private string _accountId;

    public OandaApiService(HttpClient httpClient, ILogger<OandaApiService> logger, string accountId)
    {
        _httpClient = httpClient;
        _logger = logger;
        _accountId = accountId;
    }
}
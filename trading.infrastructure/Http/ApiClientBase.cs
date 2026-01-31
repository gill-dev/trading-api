using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace trading.infrastructure.Http;

public sealed class ApiClientBase
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiClientBase> _logger;

    public ApiClientBase(HttpClient httpClient, ILogger<ApiClientBase> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string endpoint, string dataKey, CancellationToken cancellationToken = default)
     where T : class
    {
        try
        {
            using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
            return await HandleResponseAsync<T>(response, dataKey, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<T?> PutAsync<T>(string endpoint, object? body = null, string? dataKey = null, CancellationToken cancellationToken = default)
        where T : class
    {
        try
        {
            using var response = await _httpClient.PutAsJsonAsync(endpoint, body, WriteOptions, cancellationToken);
            return await HandleResponseAsync<T>(response, dataKey, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Put Request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string endpoint, object? body, string? dataKey = null, CancellationToken cancellationToken = default)
        where T : class
    {
        try
        {
            using var response = await _httpClient.PostAsJsonAsync(endpoint, body, WriteOptions, cancellationToken);
            return await HandleResponseAsync<T>(response, dataKey, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Post Request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    private async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response, string? dataKey = null, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("Request failed with {StatusCode}: {Error}", response.StatusCode, error);
            return default;
        }

        if (string.IsNullOrEmpty(dataKey))
            return await response.Content.ReadFromJsonAsync<T>(ReadOptions, cancellationToken);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);
        return json.TryGetProperty(dataKey, out var element)
            ? element.Deserialize<T>(ReadOptions)
            : default;
    }
    private static readonly JsonSerializerOptions ReadOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private static readonly JsonSerializerOptions WriteOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.WriteAsString
    };

}
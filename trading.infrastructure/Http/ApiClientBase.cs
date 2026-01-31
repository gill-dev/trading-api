using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using trading.application.Abstractions;

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

    public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        where T : class
    {
        try
        {
            using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
            return await HandleResponseAsync<T>(response, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<T?> PostAsync<T>(string endpoint, object? body, CancellationToken cancellationToken = default)
        where T : class
    {
        try
        {
            using var response = await _httpClient.PostAsJsonAsync(endpoint, body, WriteOptions, cancellationToken);
            return await HandleResponseAsync<T>(response, cancellationToken);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Post Request failed: {Endpoint}", endpoint);
            return default;
        }
    }

    public async Task<T?> PutAsync<T>(string endpoint, object? body, CancellationToken cancellationToken = default)
    where T : class
    {
        try
        {
            using var response = await _httpClient.PutAsJsonAsync(endpoint, body, WriteOptions, cancellationToken);
            return await HandleResponseAsync<T>(response, cancellationToken);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Put Request failed: {Endpoint}", endpoint);
            return default;
        }
    }


    private async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("Request failed with {error}", error);
            return default; 
        }
        
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken);
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
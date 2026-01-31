using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using trading.application.Abstractions;
using trading.infrastructure.Http;
using trading.infrastructure.Services;

namespace trading.infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Oanda").Get<OandaSettings>()
                       ?? throw new InvalidOperationException("Oanda settings not configured");

        services.AddHttpClient<ApiClientBase>(client =>
        {
            client.BaseAddress = new Uri(settings.BaseUrl);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", settings.ApiKey);
        });

        services.AddScoped<IOandaApiService>(sp =>
        {
            var client = sp.GetRequiredService<ApiClientBase>();
            return new OandaApiService(client, settings.AccountId);
        });

        return services;
    }
}
using BaleBotClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaleBotClient;

/// <summary>
/// Extension methods for registering BaleBotClient in the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers IBaleBotClient with typed configuration from appsettings.json.
    /// Configuration is read from the "BaleBot" section.
    /// </summary>
    public static IServiceCollection AddBaleBotClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BaleBotOptions>(configuration.GetSection(BaleBotOptions.SectionName));

        services.AddHttpClient<IBaleBotClient, BaleBotHttpClient>();

        return services;
    }

    /// <summary>
    /// Registers IBaleBotClient with inline options configuration.
    /// </summary>
    public static IServiceCollection AddBaleBotClient(this IServiceCollection services, Action<BaleBotOptions> configure)
    {
        services.Configure(configure);

        services.AddHttpClient<IBaleBotClient, BaleBotHttpClient>();

        return services;
    }
}

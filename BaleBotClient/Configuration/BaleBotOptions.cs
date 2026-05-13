namespace BaleBotClient.Configuration;

public sealed class BaleBotOptions
{
    public const string SectionName = "BaleBot";

    /// <summary>
    /// Bot authentication token obtained from @botfather.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Base URL for the Bale Bot API. Defaults to https://tapi.bale.ai.
    /// </summary>
    public string BaseUrl { get; set; } = "https://tapi.bale.ai";

    /// <summary>
    /// Timeout for HTTP requests in seconds. Defaults to 100.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 100;
}

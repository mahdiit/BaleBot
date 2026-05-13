namespace BaleBotClient;

/// <summary>
/// Exception thrown when the Bale Bot API returns an error response.
/// </summary>
public sealed class BaleApiException(int errorCode, string? description)
    : Exception($"Bale API error {errorCode}: {description}")
{
    public int ErrorCode { get; } = errorCode;
    public string? ApiDescription { get; } = description;
}

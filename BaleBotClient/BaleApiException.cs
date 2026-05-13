namespace BaleBotClient;

/// <summary>
/// Exception thrown when the Bale Bot API returns an error response.
/// </summary>
public sealed class BaleApiException : Exception
{
    public int ErrorCode { get; }
    public string? ApiDescription { get; }

    public BaleApiException(int errorCode, string? description)
        : base($"Bale API error {errorCode}: {description}")
    {
        ErrorCode = errorCode;
        ApiDescription = description;
    }
}

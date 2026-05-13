using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

/// <summary>
/// Generic API response wrapper. All Bale API responses follow this structure.
/// </summary>
public sealed record BaleApiResponse<T>
{
    [JsonPropertyName("ok")]
    public bool Ok { get; init; }

    [JsonPropertyName("result")]
    public T? Result { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("error_code")]
    public int? ErrorCode { get; init; }

    [JsonPropertyName("parameters")]
    public ResponseParameters? Parameters { get; init; }
}

public sealed record ResponseParameters
{
    [JsonPropertyName("retry_after")]
    public int? RetryAfter { get; init; }
}

public sealed record WebhookInfo
{
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

public sealed record WebAppData
{
    [JsonPropertyName("data")]
    public string Data { get; init; } = string.Empty;
}

public sealed record WebAppInfo
{
    [JsonPropertyName("url")]
    public string Url { get; init; } = string.Empty;
}

public sealed record CopyTextButton
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;
}

public sealed record MessageId
{
    [JsonPropertyName("message_id")]
    public int MessageIdValue { get; init; }
}

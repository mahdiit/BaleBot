using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record Update
{
    [JsonPropertyName("update_id")]
    public int UpdateId { get; init; }

    [JsonPropertyName("message")]
    public Message? Message { get; init; }

    [JsonPropertyName("edited_message")]
    public Message? EditedMessage { get; init; }

    [JsonPropertyName("callback_query")]
    public CallbackQuery? CallbackQuery { get; init; }

    [JsonPropertyName("pre_checkout_query")]
    public PreCheckoutQuery? PreCheckoutQuery { get; init; }
}

public sealed record CallbackQuery
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("from")]
    public User From { get; init; } = null!;

    [JsonPropertyName("message")]
    public Message? Message { get; init; }

    [JsonPropertyName("data")]
    public string? Data { get; init; }
}

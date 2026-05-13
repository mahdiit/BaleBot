using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record InlineKeyboardMarkup
{
    [JsonPropertyName("inline_keyboard")]
    public InlineKeyboardButton[][] InlineKeyboard { get; init; } = [];
}

public sealed record InlineKeyboardButton
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("callback_data")]
    public string? CallbackData { get; init; }

    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; init; }

    [JsonPropertyName("copy_text")]
    public CopyTextButton? CopyText { get; init; }
}

public sealed record ReplyKeyboardMarkup
{
    [JsonPropertyName("keyboard")]
    public KeyboardButton[][] Keyboard { get; init; } = [];
}

public sealed record KeyboardButton
{
    [JsonPropertyName("text")]
    public string Text { get; init; } = string.Empty;

    [JsonPropertyName("request_contact")]
    public bool? RequestContact { get; init; }

    [JsonPropertyName("request_location")]
    public bool? RequestLocation { get; init; }

    [JsonPropertyName("web_app")]
    public WebAppInfo? WebApp { get; init; }
}

public sealed record ReplyKeyboardRemove
{
    [JsonPropertyName("remove_keyboard")]
    public bool RemoveKeyboard { get; init; } = true;
}

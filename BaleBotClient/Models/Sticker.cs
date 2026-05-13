using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record Sticker
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("file_size")]
    public int? FileSize { get; init; }
}

public sealed record StickerSet
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("stickers")]
    public Sticker[] Stickers { get; init; } = [];

    [JsonPropertyName("thumbnail")]
    public PhotoSize? Thumbnail { get; init; }
}

public sealed record InputSticker
{
    [JsonPropertyName("sticker")]
    public string StickerFile { get; init; } = string.Empty;

    [JsonPropertyName("emoji_list")]
    public string[]? EmojiList { get; init; }
}

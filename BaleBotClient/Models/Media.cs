using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record PhotoSize
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
}

public sealed record Animation
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    [JsonPropertyName("thumbnail")]
    public PhotoSize? Thumbnail { get; init; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; init; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; init; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
}

public sealed record Audio
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; init; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; init; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
}

public sealed record Document
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("thumbnail")]
    public PhotoSize? Thumbnail { get; init; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; init; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; init; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
}

public sealed record Video
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("duration")]
    public int Duration { get; init; }

    [JsonPropertyName("file_name")]
    public string? FileName { get; init; }

    [JsonPropertyName("mime_type")]
    public string? MimeType { get; init; }

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
}

public sealed record Voice
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;
}

public sealed record Contact
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; init; } = string.Empty;

    [JsonPropertyName("first_name")]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }

    [JsonPropertyName("user_id")]
    public long? UserId { get; init; }
}

public sealed record Location
{
    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }
}

public sealed record BaleFile
{
    [JsonPropertyName("file_id")]
    public string FileId { get; init; } = string.Empty;

    [JsonPropertyName("file_unique_id")]
    public string FileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }

    [JsonPropertyName("file_path")]
    public string? FilePath { get; init; }
}

// InputMedia types for sendMediaGroup
[JsonDerivedType(typeof(InputMediaPhoto))]
[JsonDerivedType(typeof(InputMediaVideo))]
[JsonDerivedType(typeof(InputMediaAnimation))]
[JsonDerivedType(typeof(InputMediaAudio))]
[JsonDerivedType(typeof(InputMediaDocument))]
public abstract record InputMedia
{
    [JsonPropertyName("type")]
    public abstract string Type { get; }

    [JsonPropertyName("media")]
    public string Media { get; init; } = string.Empty;

    [JsonPropertyName("caption")]
    public string? Caption { get; init; }
}

public sealed record InputMediaPhoto : InputMedia
{
    [JsonPropertyName("type")]
    public override string Type => "photo";
}

public sealed record InputMediaVideo : InputMedia
{
    [JsonPropertyName("type")]
    public override string Type => "video";

    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; init; }

    [JsonPropertyName("width")]
    public int? Width { get; init; }

    [JsonPropertyName("height")]
    public int? Height { get; init; }

    [JsonPropertyName("duration")]
    public int? Duration { get; init; }
}

public sealed record InputMediaAnimation : InputMedia
{
    [JsonPropertyName("type")]
    public override string Type => "animation";

    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; init; }

    [JsonPropertyName("width")]
    public int? Width { get; init; }

    [JsonPropertyName("height")]
    public int? Height { get; init; }

    [JsonPropertyName("duration")]
    public int? Duration { get; init; }
}

public sealed record InputMediaAudio : InputMedia
{
    [JsonPropertyName("type")]
    public override string Type => "audio";

    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; init; }

    [JsonPropertyName("duration")]
    public int? Duration { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }
}

public sealed record InputMediaDocument : InputMedia
{
    [JsonPropertyName("type")]
    public override string Type => "document";

    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; init; }
}

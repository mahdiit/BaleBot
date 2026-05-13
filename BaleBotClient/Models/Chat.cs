using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record Chat
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }
}

public sealed record ChatFullInfo
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }

    [JsonPropertyName("photo")]
    public ChatPhoto? Photo { get; init; }

    [JsonPropertyName("bio")]
    public string? Bio { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("invite_link")]
    public string? InviteLink { get; init; }

    [JsonPropertyName("linked_chat_id")]
    public string? LinkedChatId { get; init; }
}

public sealed record ChatPhoto
{
    [JsonPropertyName("small_file_id")]
    public string SmallFileId { get; init; } = string.Empty;

    [JsonPropertyName("small_file_unique_id")]
    public string SmallFileUniqueId { get; init; } = string.Empty;

    [JsonPropertyName("big_file_id")]
    public string BigFileId { get; init; } = string.Empty;

    [JsonPropertyName("big_file_unique_id")]
    public string BigFileUniqueId { get; init; } = string.Empty;
}

public sealed record ChatInviteLink
{
    [JsonPropertyName("invite_link")]
    public string InviteLink { get; init; } = string.Empty;
}

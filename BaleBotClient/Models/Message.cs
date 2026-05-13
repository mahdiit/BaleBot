using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record Message
{
    [JsonPropertyName("message_id")]
    public int MessageId { get; init; }

    [JsonPropertyName("from")]
    public User? From { get; init; }

    [JsonPropertyName("date")]
    public int Date { get; init; }

    [JsonPropertyName("chat")]
    public Chat Chat { get; init; } = null!;

    [JsonPropertyName("sender_chat")]
    public Chat? SenderChat { get; init; }

    [JsonPropertyName("forward_from")]
    public User? ForwardFrom { get; init; }

    [JsonPropertyName("forward_from_chat")]
    public Chat? ForwardFromChat { get; init; }

    [JsonPropertyName("forward_from_message_id")]
    public int? ForwardFromMessageId { get; init; }

    [JsonPropertyName("forward_date")]
    public int? ForwardDate { get; init; }

    [JsonPropertyName("reply_to_message")]
    public Message? ReplyToMessage { get; init; }

    [JsonPropertyName("edit_date")]
    public int? EditDate { get; init; }

    [JsonPropertyName("media_group_id")]
    public string? MediaGroupId { get; init; }

    [JsonPropertyName("text")]
    public string? Text { get; init; }

    [JsonPropertyName("entities")]
    public MessageEntity[]? Entities { get; init; }

    [JsonPropertyName("animation")]
    public Animation? Animation { get; init; }

    [JsonPropertyName("audio")]
    public Audio? Audio { get; init; }

    [JsonPropertyName("document")]
    public Document? Document { get; init; }

    [JsonPropertyName("photo")]
    public PhotoSize[]? Photo { get; init; }

    [JsonPropertyName("sticker")]
    public Sticker? Sticker { get; init; }

    [JsonPropertyName("video")]
    public Video? Video { get; init; }

    [JsonPropertyName("voice")]
    public Voice? Voice { get; init; }

    [JsonPropertyName("caption")]
    public string? Caption { get; init; }

    [JsonPropertyName("caption_entities")]
    public MessageEntity[]? CaptionEntities { get; init; }

    [JsonPropertyName("contact")]
    public Contact? Contact { get; init; }

    [JsonPropertyName("location")]
    public Location? Location { get; init; }

    [JsonPropertyName("new_chat_members")]
    public User[]? NewChatMembers { get; init; }

    [JsonPropertyName("left_chat_member")]
    public User? LeftChatMember { get; init; }

    [JsonPropertyName("invoice")]
    public Invoice? Invoice { get; init; }

    [JsonPropertyName("successful_payment")]
    public SuccessfulPayment? SuccessfulPayment { get; init; }

    [JsonPropertyName("web_app_data")]
    public WebAppData? WebAppData { get; init; }

    [JsonPropertyName("reply_markup")]
    public InlineKeyboardMarkup? ReplyMarkup { get; init; }
}

public sealed record MessageEntity
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("offset")]
    public int Offset { get; init; }

    [JsonPropertyName("length")]
    public int Length { get; init; }
}

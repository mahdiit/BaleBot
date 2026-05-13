using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "status")]
[JsonDerivedType(typeof(ChatMemberOwner), "creator")]
[JsonDerivedType(typeof(ChatMemberAdministrator), "administrator")]
[JsonDerivedType(typeof(ChatMemberMember), "member")]
[JsonDerivedType(typeof(ChatMemberRestricted), "restricted")]
public abstract record ChatMember
{
    [JsonPropertyName("status")]
    public abstract string Status { get; }

    [JsonPropertyName("user")]
    public User User { get; init; } = null!;
}

public sealed record ChatMemberOwner : ChatMember
{
    public override string Status => "creator";
}

public sealed record ChatMemberAdministrator : ChatMember
{
    public override string Status => "administrator";

    [JsonPropertyName("can_delete_messages")]
    public bool CanDeleteMessages { get; init; }

    [JsonPropertyName("can_manage_video_chats")]
    public bool CanManageVideoChats { get; init; }

    [JsonPropertyName("can_restrict_members")]
    public bool CanRestrictMembers { get; init; }

    [JsonPropertyName("can_promote_members")]
    public bool CanPromoteMembers { get; init; }

    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; init; }

    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; init; }

    [JsonPropertyName("can_post_stories")]
    public bool CanPostStories { get; init; }

    [JsonPropertyName("can_post_messages")]
    public bool? CanPostMessages { get; init; }

    [JsonPropertyName("can_edit_messages")]
    public bool? CanEditMessages { get; init; }

    [JsonPropertyName("can_pin_messages")]
    public bool? CanPinMessages { get; init; }
}

public sealed record ChatMemberMember : ChatMember
{
    public override string Status => "member";
}

public sealed record ChatMemberRestricted : ChatMember
{
    public override string Status => "restricted";

    [JsonPropertyName("is_member")]
    public bool IsMember { get; init; }

    [JsonPropertyName("can_send_messages")]
    public bool CanSendMessages { get; init; }

    [JsonPropertyName("can_send_audios")]
    public bool CanSendAudios { get; init; }

    [JsonPropertyName("can_send_documents")]
    public bool CanSendDocuments { get; init; }

    [JsonPropertyName("can_send_photos")]
    public bool CanSendPhotos { get; init; }

    [JsonPropertyName("can_send_videos")]
    public bool CanSendVideos { get; init; }

    [JsonPropertyName("can_change_info")]
    public bool CanChangeInfo { get; init; }

    [JsonPropertyName("can_invite_users")]
    public bool CanInviteUsers { get; init; }

    [JsonPropertyName("can_pin_messages")]
    public bool CanPinMessages { get; init; }
}

using BaleBotClient.Models;

namespace BaleBotClient;

/// <summary>
/// Client interface for the Bale Bot API.
/// </summary>
public interface IBaleBotClient
{
    // ── Bot Info ──
    Task<User> GetMeAsync(CancellationToken ct = default);

    // ── Updates ──
    Task<Update[]> GetUpdatesAsync(int? offset = null, int? limit = null, int? timeout = null, CancellationToken ct = default);
    Task<bool> SetWebhookAsync(string url, CancellationToken ct = default);
    Task<bool> DeleteWebhookAsync(CancellationToken ct = default);
    Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken ct = default);

    // ── Send Methods ──
    Task<Message> SendMessageAsync(long chatId, string text, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> ForwardMessageAsync(long chatId, long fromChatId, int messageId, CancellationToken ct = default);
    Task<MessageId> CopyMessageAsync(long chatId, long fromChatId, int messageId, CancellationToken ct = default);
    Task<Message> SendPhotoAsync(long chatId, string photo, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendAudioAsync(long chatId, string audio, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendDocumentAsync(long chatId, string document, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendVideoAsync(long chatId, string video, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendAnimationAsync(long chatId, string animation, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendVoiceAsync(long chatId, string voice, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message[]> SendMediaGroupAsync(long chatId, InputMedia[] media, int? replyToMessageId = null, CancellationToken ct = default);
    Task<Message> SendLocationAsync(long chatId, double latitude, double longitude, double? horizontalAccuracy = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<Message> SendContactAsync(long chatId, string phoneNumber, string firstName, string? lastName = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default);
    Task<bool> SendChatActionAsync(long chatId, string action, CancellationToken ct = default);

    // ── File ──
    Task<BaleFile> GetFileAsync(string fileId, CancellationToken ct = default);
    string GetFileDownloadUrl(string filePath);

    // ── Callback ──
    Task<bool> AnswerCallbackQueryAsync(string callbackQueryId, string? text = null, bool? showAlert = null, CancellationToken ct = default);

    // ── Review ──
    Task<bool> AskReviewAsync(long userId, int delaySeconds, CancellationToken ct = default);

    // ── Chat Administration ──
    Task<bool> BanChatMemberAsync(long chatId, long userId, CancellationToken ct = default);
    Task<bool> UnbanChatMemberAsync(long chatId, long userId, bool? onlyIfBanned = null, CancellationToken ct = default);
    Task<bool> PromoteChatMemberAsync(long chatId, long userId, bool? canChangeInfo = null, bool? canPostMessages = null, bool? canEditMessages = null, bool? canDeleteMessages = null, bool? canManageVideoChats = null, bool? canInviteUsers = null, bool? canRestrictMembers = null, CancellationToken ct = default);
    Task<bool> SetChatPhotoAsync(long chatId, Stream photo, string fileName, CancellationToken ct = default);
    Task<bool> LeaveChatAsync(long chatId, CancellationToken ct = default);
    Task<ChatFullInfo> GetChatAsync(long chatId, CancellationToken ct = default);
    Task<ChatMember[]> GetChatAdministratorsAsync(long chatId, CancellationToken ct = default);
    Task<int> GetChatMembersCountAsync(long chatId, CancellationToken ct = default);
    Task<ChatMember> GetChatMemberAsync(long chatId, long userId, CancellationToken ct = default);
    Task<bool> PinChatMessageAsync(long chatId, int messageId, CancellationToken ct = default);
    Task<bool> UnpinChatMessageAsync(long chatId, int messageId, CancellationToken ct = default);
    Task<bool> UnpinAllChatMessagesAsync(long chatId, CancellationToken ct = default);
    Task<bool> SetChatTitleAsync(long chatId, string title, CancellationToken ct = default);
    Task<bool> SetChatDescriptionAsync(long chatId, string description, CancellationToken ct = default);
    Task<bool> DeleteChatPhotoAsync(long chatId, CancellationToken ct = default);
    Task<ChatInviteLink> CreateChatInviteLinkAsync(long chatId, CancellationToken ct = default);
    Task<ChatInviteLink> RevokeChatInviteLinkAsync(long chatId, string inviteLink, CancellationToken ct = default);
    Task<string> ExportChatInviteLinkAsync(long chatId, CancellationToken ct = default);

    // ── Edit Messages ──
    Task<Message> EditMessageTextAsync(long chatId, int messageId, string text, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default);
    Task<Message> EditMessageCaptionAsync(long chatId, int messageId, string? caption = null, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default);
    Task<Message> EditMessageReplyMarkupAsync(long chatId, int messageId, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default);
    Task<bool> DeleteMessageAsync(long chatId, int messageId, CancellationToken ct = default);

    // ── Stickers ──
    Task<BaleFile> UploadStickerFileAsync(long userId, Stream sticker, string fileName, CancellationToken ct = default);
    Task<bool> CreateNewStickerSetAsync(long userId, string name, string title, InputSticker[] stickers, CancellationToken ct = default);
    Task<bool> AddStickerToSetAsync(long userId, string name, InputSticker sticker, CancellationToken ct = default);

    // ── Payments ──
    Task<Message> SendInvoiceAsync(long chatId, string title, string description, string payload, string providerToken, LabeledPrice[] prices, string? photoUrl = null, int? replyToMessageId = null, CancellationToken ct = default);
    Task<string> CreateInvoiceLinkAsync(string title, string description, string payload, string providerToken, LabeledPrice[] prices, CancellationToken ct = default);
    Task<bool> AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, bool ok, string? errorMessage = null, CancellationToken ct = default);
    Task<Transaction> InquireTransactionAsync(string transactionId, CancellationToken ct = default);
}

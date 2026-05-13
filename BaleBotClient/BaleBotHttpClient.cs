using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaleBotClient.Configuration;
using BaleBotClient.Models;
using Microsoft.Extensions.Options;

namespace BaleBotClient;

/// <summary>
/// HttpClient-based implementation of the Bale Bot API client.
/// </summary>
public sealed class BaleBotHttpClient : IBaleBotClient
{
    private readonly HttpClient _http;
    private readonly BaleBotOptions _options;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public BaleBotHttpClient(HttpClient http, IOptions<BaleBotOptions> options)
    {
        _http = http;
        _options = options.Value;
        _http.BaseAddress = new Uri($"{_options.BaseUrl.TrimEnd('/')}/bot{_options.Token}/");
        _http.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    // ── Helpers ──────────────────────────────────────────────────────

    private async Task<T> PostAsync<T>(string method, object? payload, CancellationToken ct)
    {
        HttpResponseMessage response;
        if (payload is null)
            response = await _http.PostAsync(method, null, ct).ConfigureAwait(false);
        else
            response = await _http.PostAsJsonAsync(method, payload, JsonOptions, ct).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var apiResponse = await response.Content
            .ReadFromJsonAsync<BaleApiResponse<T>>(JsonOptions, ct)
            .ConfigureAwait(false);

        if (apiResponse is null || !apiResponse.Ok)
            throw new BaleApiException(apiResponse?.ErrorCode ?? -1, apiResponse?.Description);

        return apiResponse.Result!;
    }

    private Task<T> GetAsync<T>(string method, CancellationToken ct)
        => PostAsync<T>(method, null, ct);

    private async Task<T> PostMultipartAsync<T>(string method, MultipartFormDataContent content, CancellationToken ct)
    {
        var response = await _http.PostAsync(method, content, ct).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var apiResponse = await response.Content
            .ReadFromJsonAsync<BaleApiResponse<T>>(JsonOptions, ct)
            .ConfigureAwait(false);

        if (apiResponse is null || !apiResponse.Ok)
            throw new BaleApiException(apiResponse?.ErrorCode ?? -1, apiResponse?.Description);

        return apiResponse.Result!;
    }

    // ── Bot Info ─────────────────────────────────────────────────────

    public Task<User> GetMeAsync(CancellationToken ct = default)
        => GetAsync<User>("getMe", ct);

    // ── Updates ─────────────────────────────────────────────────────

    public Task<Update[]> GetUpdatesAsync(int? offset = null, int? limit = null, int? timeout = null, CancellationToken ct = default)
        => PostAsync<Update[]>("getUpdates", new { offset, limit, timeout }, ct);

    public Task<bool> SetWebhookAsync(string url, CancellationToken ct = default)
        => PostAsync<bool>("setWebhook", new { url }, ct);

    public Task<bool> DeleteWebhookAsync(CancellationToken ct = default)
        => GetAsync<bool>("deleteWebhook", ct);

    public Task<WebhookInfo> GetWebhookInfoAsync(CancellationToken ct = default)
        => GetAsync<WebhookInfo>("getWebhookInfo", ct);

    // ── Send Methods ────────────────────────────────────────────────

    public Task<Message> SendMessageAsync(long chatId, string text, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendMessage", new
        {
            chat_id = chatId,
            text,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> ForwardMessageAsync(long chatId, long fromChatId, int messageId, CancellationToken ct = default)
        => PostAsync<Message>("forwardMessage", new
        {
            chat_id = chatId,
            from_chat_id = fromChatId,
            message_id = messageId
        }, ct);

    public Task<MessageId> CopyMessageAsync(long chatId, long fromChatId, int messageId, CancellationToken ct = default)
        => PostAsync<MessageId>("copyMessage", new
        {
            chat_id = chatId,
            from_chat_id = fromChatId,
            message_id = messageId
        }, ct);

    public Task<Message> SendPhotoAsync(long chatId, string photo, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendPhoto", new
        {
            chat_id = chatId,
            photo,
            caption,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendAudioAsync(long chatId, string audio, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendAudio", new
        {
            chat_id = chatId,
            audio,
            caption,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendDocumentAsync(long chatId, string document, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendDocument", new
        {
            chat_id = chatId,
            document,
            caption,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendVideoAsync(long chatId, string video, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendVideo", new
        {
            chat_id = chatId,
            video,
            caption,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendAnimationAsync(long chatId, string animation, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendAnimation", new
        {
            chat_id = chatId,
            animation,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendVoiceAsync(long chatId, string voice, string? caption = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendVoice", new
        {
            chat_id = chatId,
            voice,
            caption,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message[]> SendMediaGroupAsync(long chatId, InputMedia[] media, int? replyToMessageId = null, CancellationToken ct = default)
        => PostAsync<Message[]>("sendMediaGroup", new
        {
            chat_id = chatId,
            media,
            reply_to_message_id = replyToMessageId
        }, ct);

    public Task<Message> SendLocationAsync(long chatId, double latitude, double longitude, double? horizontalAccuracy = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendLocation", new
        {
            chat_id = chatId,
            latitude,
            longitude,
            horizontal_accuracy = horizontalAccuracy,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> SendContactAsync(long chatId, string phoneNumber, string firstName, string? lastName = null, int? replyToMessageId = null, object? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("sendContact", new
        {
            chat_id = chatId,
            phone_number = phoneNumber,
            first_name = firstName,
            last_name = lastName,
            reply_to_message_id = replyToMessageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<bool> SendChatActionAsync(long chatId, string action, CancellationToken ct = default)
        => PostAsync<bool>("sendChatAction", new { chat_id = chatId, action }, ct);

    // ── File ────────────────────────────────────────────────────────

    public Task<BaleFile> GetFileAsync(string fileId, CancellationToken ct = default)
        => PostAsync<BaleFile>("getFile", new { file_id = fileId }, ct);

    public string GetFileDownloadUrl(string filePath)
        => $"{_options.BaseUrl.TrimEnd('/')}/file/bot{_options.Token}/{filePath}";

    // ── Callback ────────────────────────────────────────────────────

    public Task<bool> AnswerCallbackQueryAsync(string callbackQueryId, string? text = null, bool? showAlert = null, CancellationToken ct = default)
        => PostAsync<bool>("answerCallbackQuery", new
        {
            callback_query_id = callbackQueryId,
            text,
            show_alert = showAlert
        }, ct);

    // ── Review ──────────────────────────────────────────────────────

    public Task<bool> AskReviewAsync(long userId, int delaySeconds, CancellationToken ct = default)
        => PostAsync<bool>("askReview", new { user_id = userId, delay_seconds = delaySeconds }, ct);

    // ── Chat Administration ─────────────────────────────────────────

    public Task<bool> BanChatMemberAsync(long chatId, long userId, CancellationToken ct = default)
        => PostAsync<bool>("banChatMember", new { chat_id = chatId, user_id = userId }, ct);

    public Task<bool> UnbanChatMemberAsync(long chatId, long userId, bool? onlyIfBanned = null, CancellationToken ct = default)
        => PostAsync<bool>("unbanChatMember", new { chat_id = chatId, user_id = userId, only_if_banned = onlyIfBanned }, ct);

    public Task<bool> PromoteChatMemberAsync(long chatId, long userId,
        bool? canChangeInfo = null, bool? canPostMessages = null,
        bool? canEditMessages = null, bool? canDeleteMessages = null,
        bool? canManageVideoChats = null, bool? canInviteUsers = null,
        bool? canRestrictMembers = null, CancellationToken ct = default)
        => PostAsync<bool>("promoteChatMember", new
        {
            chat_id = chatId,
            user_id = userId,
            can_change_info = canChangeInfo,
            can_post_messages = canPostMessages,
            can_edit_messages = canEditMessages,
            can_delete_messages = canDeleteMessages,
            can_manage_video_chats = canManageVideoChats,
            can_invite_users = canInviteUsers,
            can_restrict_members = canRestrictMembers
        }, ct);

    public async Task<bool> SetChatPhotoAsync(long chatId, Stream photo, string fileName, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent
        {
            { new StringContent(chatId.ToString()), "chat_id" },
            { new StreamContent(photo), "photo", fileName }
        };
        return await PostMultipartAsync<bool>("setChatPhoto", content, ct).ConfigureAwait(false);
    }

    public Task<bool> LeaveChatAsync(long chatId, CancellationToken ct = default)
        => PostAsync<bool>("leaveChat", new { chat_id = chatId }, ct);

    public Task<ChatFullInfo> GetChatAsync(long chatId, CancellationToken ct = default)
        => PostAsync<ChatFullInfo>("getChat", new { chat_id = chatId }, ct);

    public Task<ChatMember[]> GetChatAdministratorsAsync(long chatId, CancellationToken ct = default)
        => PostAsync<ChatMember[]>("getChatAdministrators", new { chat_id = chatId }, ct);

    public Task<int> GetChatMembersCountAsync(long chatId, CancellationToken ct = default)
        => PostAsync<int>("getChatMembersCount", new { chat_id = chatId }, ct);

    public Task<ChatMember> GetChatMemberAsync(long chatId, long userId, CancellationToken ct = default)
        => PostAsync<ChatMember>("getChatMember", new { chat_id = chatId, user_id = userId }, ct);

    public Task<bool> PinChatMessageAsync(long chatId, int messageId, CancellationToken ct = default)
        => PostAsync<bool>("pinChatMessage", new { chat_id = chatId, message_id = messageId }, ct);

    public Task<bool> UnpinChatMessageAsync(long chatId, int messageId, CancellationToken ct = default)
        => PostAsync<bool>("unPinChatMessage", new { chat_id = chatId, message_id = messageId }, ct);

    public Task<bool> UnpinAllChatMessagesAsync(long chatId, CancellationToken ct = default)
        => PostAsync<bool>("unpinAllChatMessages", new { chat_id = chatId }, ct);

    public Task<bool> SetChatTitleAsync(long chatId, string title, CancellationToken ct = default)
        => PostAsync<bool>("setChatTitle", new { chat_id = chatId, title }, ct);

    public Task<bool> SetChatDescriptionAsync(long chatId, string description, CancellationToken ct = default)
        => PostAsync<bool>("setChatDescription", new { chat_id = chatId, description }, ct);

    public Task<bool> DeleteChatPhotoAsync(long chatId, CancellationToken ct = default)
        => PostAsync<bool>("deleteChatPhoto", new { chat_id = chatId }, ct);

    public Task<ChatInviteLink> CreateChatInviteLinkAsync(long chatId, CancellationToken ct = default)
        => PostAsync<ChatInviteLink>("createChatInviteLink", new { chat_id = chatId }, ct);

    public Task<ChatInviteLink> RevokeChatInviteLinkAsync(long chatId, string inviteLink, CancellationToken ct = default)
        => PostAsync<ChatInviteLink>("revokeChatInviteLink", new { chat_id = chatId, invite_link = inviteLink }, ct);

    public Task<string> ExportChatInviteLinkAsync(long chatId, CancellationToken ct = default)
        => PostAsync<string>("exportChatInviteLink", new { chat_id = chatId }, ct);

    // ── Edit Messages ───────────────────────────────────────────────

    public Task<Message> EditMessageTextAsync(long chatId, int messageId, string text, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("editMessageText", new
        {
            chat_id = chatId,
            message_id = messageId,
            text,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> EditMessageCaptionAsync(long chatId, int messageId, string? caption = null, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("editMessageCaption", new
        {
            chat_id = chatId,
            message_id = messageId,
            caption,
            reply_markup = replyMarkup
        }, ct);

    public Task<Message> EditMessageReplyMarkupAsync(long chatId, int messageId, InlineKeyboardMarkup? replyMarkup = null, CancellationToken ct = default)
        => PostAsync<Message>("editMessageReplyMarkup", new
        {
            chat_id = chatId,
            message_id = messageId,
            reply_markup = replyMarkup
        }, ct);

    public Task<bool> DeleteMessageAsync(long chatId, int messageId, CancellationToken ct = default)
        => PostAsync<bool>("deleteMessage", new { chat_id = chatId, message_id = messageId }, ct);

    // ── Stickers ────────────────────────────────────────────────────

    public async Task<BaleFile> UploadStickerFileAsync(long userId, Stream sticker, string fileName, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent
        {
            { new StringContent(userId.ToString()), "user_id" },
            { new StreamContent(sticker), "sticker", fileName }
        };
        return await PostMultipartAsync<BaleFile>("uploadStickerFile", content, ct).ConfigureAwait(false);
    }

    public Task<bool> CreateNewStickerSetAsync(long userId, string name, string title, InputSticker[] stickers, CancellationToken ct = default)
        => PostAsync<bool>("createNewStickerSet", new
        {
            user_id = userId,
            name,
            title,
            sticker = stickers
        }, ct);

    public Task<bool> AddStickerToSetAsync(long userId, string name, InputSticker sticker, CancellationToken ct = default)
        => PostAsync<bool>("addStickerToSet", new
        {
            user_id = userId,
            name,
            sticker
        }, ct);

    // ── Payments ────────────────────────────────────────────────────

    public Task<Message> SendInvoiceAsync(long chatId, string title, string description, string payload, string providerToken, LabeledPrice[] prices, string? photoUrl = null, int? replyToMessageId = null, CancellationToken ct = default)
        => PostAsync<Message>("sendInvoice", new
        {
            chat_id = chatId,
            title,
            description,
            payload,
            provider_token = providerToken,
            prices,
            photo_url = photoUrl,
            reply_to_message_id = replyToMessageId
        }, ct);

    public Task<string> CreateInvoiceLinkAsync(string title, string description, string payload, string providerToken, LabeledPrice[] prices, CancellationToken ct = default)
        => PostAsync<string>("createInvoiceLink", new
        {
            title,
            description,
            payload,
            provider_token = providerToken,
            prices
        }, ct);

    public Task<bool> AnswerPreCheckoutQueryAsync(string preCheckoutQueryId, bool ok, string? errorMessage = null, CancellationToken ct = default)
        => PostAsync<bool>("answerPreCheckoutQuery", new
        {
            pre_checkout_query_id = preCheckoutQueryId,
            ok,
            error_message = errorMessage
        }, ct);

    public Task<Transaction> InquireTransactionAsync(string transactionId, CancellationToken ct = default)
        => PostAsync<Transaction>("inquireTransaction", new { transaction_id = transactionId }, ct);
}

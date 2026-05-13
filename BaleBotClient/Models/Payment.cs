using System.Text.Json.Serialization;

namespace BaleBotClient.Models;

public sealed record Invoice
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("total_amount")]
    public int TotalAmount { get; init; }
}

public sealed record LabeledPrice
{
    [JsonPropertyName("label")]
    public string Label { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public int Amount { get; init; }
}

public sealed record PreCheckoutQuery
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("from")]
    public User From { get; init; } = null!;

    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;

    [JsonPropertyName("total_amount")]
    public int TotalAmount { get; init; }

    [JsonPropertyName("invoice_payload")]
    public string InvoicePayload { get; init; } = string.Empty;
}

public sealed record SuccessfulPayment
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; } = string.Empty;

    [JsonPropertyName("total_amount")]
    public int TotalAmount { get; init; }

    [JsonPropertyName("invoice_payload")]
    public string InvoicePayload { get; init; } = string.Empty;

    [JsonPropertyName("telegram_payment_charge_id")]
    public string TelegramPaymentChargeId { get; init; } = string.Empty;

    [JsonPropertyName("provider_payment_charge_id")]
    public string ProviderPaymentChargeId { get; init; } = string.Empty;
}

public sealed record Transaction
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("userID")]
    public long UserId { get; init; }

    [JsonPropertyName("amount")]
    public int Amount { get; init; }

    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; init; }
}

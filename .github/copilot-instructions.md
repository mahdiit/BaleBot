# Copilot Instructions — BaleBotClient

## Project Overview

A C# class library wrapping the [Bale Messenger Bot API](https://docs.bale.ai) (Telegram-compatible with minor differences). It provides a typed `HttpClient`-based client with DI integration for ASP.NET Core / generic host applications.

## Build

```bash
cd BaleBotClient
dotnet build
```

No test project exists yet. No linter or CI pipeline is configured.

## Architecture

**Single-project library** (`BaleBotClient/`) with three layers:

1. **Models** (`Models/`) — Immutable `record` types matching the Bale API JSON schema. Each file groups related types (e.g., `Media.cs` has `PhotoSize`, `Audio`, `Video`, `InputMedia*` hierarchy; `Payment.cs` has `Invoice`, `LabeledPrice`, `Transaction`).

2. **Client contract** (`IBaleBotClient.cs`) — Interface declaring every API method, organized by section comments (`// ── Send Methods ──`, `// ── Chat Administration ──`, etc.).

3. **Client implementation** (`BaleBotHttpClient.cs`) — Uses three private helpers:
   - `PostAsync<T>` — JSON POST via `PostAsJsonAsync`, deserializes `BaleApiResponse<T>` wrapper
   - `GetAsync<T>` — delegates to `PostAsync` with null payload
   - `PostMultipartAsync<T>` — for file uploads (`setChatPhoto`, `uploadStickerFile`)

**DI registration** (`ServiceCollectionExtensions.cs`) — Two `AddBaleBotClient` overloads:
- From `IConfiguration` (reads `"BaleBot"` section)
- From `Action<BaleBotOptions>` lambda

## Key Conventions

- **All API model types are `sealed record`** with `[JsonPropertyName]` attributes using snake_case. Do not use `JsonNamingPolicy` on individual properties — the explicit attributes take precedence.
- **`BaleBotHttpClient` constructs its `BaseAddress`** as `{BaseUrl}/bot{Token}/` in the constructor. API method names are passed as relative URIs (e.g., `"sendMessage"`).
- **All async methods accept a trailing `CancellationToken ct = default`** parameter.
- **Nullable optional fields** use `T?` with `JsonIgnoreCondition.WhenWritingNull` so nulls are omitted from request payloads.
- **`replyMarkup` parameters** are typed as `object?` on send methods (accepts `InlineKeyboardMarkup`, `ReplyKeyboardMarkup`, or `ReplyKeyboardRemove`) but as `InlineKeyboardMarkup?` on edit methods.
- **Polymorphic deserialization** uses `[JsonDerivedType]` / `[JsonPolymorphic]` on `ChatMember` (discriminator: `"status"`) and `InputMedia` (discriminator: `"type"`).
- **Error handling**: API errors throw `BaleApiException` with `ErrorCode` and `ApiDescription`.

## Bale API Reference

The full API spec is in `bale-docs.md` at the repo root (Persian). The API base URL is `https://tapi.bale.ai/bot<token>/METHOD_NAME`. File downloads use `https://tapi.bale.ai/file/bot<token>/<file_path>`.

When adding new API methods:
1. Add the method signature to `IBaleBotClient.cs` in the appropriate section
2. Implement in `BaleBotHttpClient.cs` using `PostAsync<T>` (or `PostMultipartAsync<T>` for file uploads)
3. Add any new model types as `sealed record` in the matching `Models/` file

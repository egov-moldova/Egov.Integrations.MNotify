# Egov.Integrations.MNotify.Extended

[![NuGet](https://img.shields.io/nuget/v/Egov.Integrations.MNotify.Extended.svg)](https://www.nuget.org/packages/Egov.Integrations.MNotify.Extended)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/egov-moldova/Egov.Integrations.MNotify/blob/master/LICENSE)

An extension of [`Egov.Integrations.MNotify`](https://www.nuget.org/packages/Egov.Integrations.MNotify) that adds the recipient-facing side of the MNotify service: reading a user's message inbox, marking messages as read, managing recipient contacts, and setting the recipient's preferred language.

`IMNotifyExtendedClient` derives from `IMNotifyClient`, so a single injected client covers both the sending API of the base library and the recipient API added here.

---

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
  - [Dependency Injection (Recommended)](#dependency-injection-recommended)
  - [Reading Messages](#reading-messages)
  - [Managing Contacts](#managing-contacts)
  - [Recipient Language](#recipient-language)
- [Error Handling](#error-handling)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [AI Assistance](#ai-assistance)
- [License](#license)

---

## Features

- **Message Inbox**: List a recipient's notification messages, with or without pagination summary, and fetch a single message with its body and attachments.
- **Read State**: Mark an individual message as read, or mark every message of a recipient as read.
- **Contact Management**: List a recipient's contacts and register new ones (email, phone, push device, …).
- **Language Preference**: Update a recipient's preferred notification language (Ro / En / Ru).
- **Superset of the Base Client**: `IMNotifyExtendedClient : IMNotifyClient` — sending notifications and managing templates continue to work through the same instance.
- **Certificate-based Auth**: Shares the mTLS setup and `MNotifyClientOptions` of `Egov.Integrations.MNotify` via `Egov.Extensions.Configuration`.
- **Async-first API**: Fully asynchronous methods for all service operations.
- **Built for .NET 10+**: Leverages the latest .NET features and performance improvements.

---

## Prerequisites

- .NET 10.0 or later
- A valid service certificate for MNotify (PFX or PEM format)
- Access to the MNotify service API
- `Egov.Extensions.Configuration` for certificate management

---

## Installation

Install the package from [NuGet](https://www.nuget.org/packages/Egov.Integrations.MNotify.Extended):

```shell
dotnet add package Egov.Integrations.MNotify.Extended
```

Or via the Package Manager Console:

```shell
Install-Package Egov.Integrations.MNotify.Extended
```

`Egov.Integrations.MNotify` is pulled in as a dependency — do not reference it separately.

---

## Configuration

Configuration is identical to the base library. Add the following sections to your **appsettings.json**:

```json
{
  "MNotify": {
    "BaseAddress": "https://mnotify.api.example.com"
  },
  "Certificate": {
    "Path": "Files/Certificates/your-certificate.pfx",
    "Password": "your-certificate-password"
  }
}
```

The client automatically uses the certificate configured via `Egov.Extensions.Configuration`.

---

## Usage

### Dependency Injection (Recommended)

Register the certificate and the extended MNotify client in **Program.cs**:

```csharp
using Egov.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register the system certificate (required for mTLS)
builder.Services.AddSystemCertificate(builder.Configuration.GetSection("Certificate"));

// Register the extended MNotify client
builder.Services.AddMNotifyExtendedClient(builder.Configuration.GetSection("MNotify"));

var app = builder.Build();
```

Call `AddMNotifyExtendedClient` *instead of* `AddMNotifyClient` — it registers the same options and HTTP pipeline, and resolves `IMNotifyExtendedClient`.

### Reading Messages

Inject `IMNotifyExtendedClient` and work with a recipient's inbox:

```csharp
public class InboxService
{
    private readonly IMNotifyExtendedClient _client;

    public InboxService(IMNotifyExtendedClient client)
    {
        _client = client;
    }

    public async Task ShowInboxAsync(string userId)
    {
        var pagination = new NotificationPagination { Page = 1, ItemsPerPage = 10 };

        // Message headers only
        IList<NotificationMessageInfo>? messages =
            await _client.GetNotificationMessagesAsync(userId, pagination);

        // Same list, plus total/unread counters
        NotificationMessagesFullInfo? page =
            await _client.GetNotificationMessagesFullInfoAsync(userId, pagination);

        int unread = page?.PagedSummary.UnreadMessages ?? 0;
    }

    public async Task OpenMessageAsync(string userId, Guid messageId)
    {
        NotificationMessage? message = await _client.GetNotificationMessageAsync(userId, messageId);

        // Mark this one as read
        await _client.UpdateMessageReadFlagAsync(userId, messageId);
    }

    public async Task ClearBadgeAsync(string userId)
        => await _client.MarkAllMessagesAsRead(userId);
}
```

### Managing Contacts

```csharp
public async Task ManageContactsAsync(IMNotifyExtendedClient client, string userId)
{
    IList<ContactInfo>? contacts = await client.GetContactsAsync(userId);

    var contact = new NotificationContact
    {
        Value = "user@example.md",
        Channel = "Email",
        Preferences = "All",
        Recipient = "1234567890123",
        RecipientType = NotificationRecipientType.IDNP,
        RecipientFirstName = "Ion",
        RecipientLastName = "Popescu"
    };

    int contactId = await client.CreateContactAsync(contact);
}
```

### Recipient Language

```csharp
await client.EditLanguageAsync(userId, NotificationLanguage.Ro);
```

---

## Error Handling

Like the base library, the client throws `ApplicationException` in most error scenarios, wrapping the original response body or exception:

| Scenario | Exception |
|----------|-----------|
| Certificate not configured | `ApplicationException` |
| Invalid API response (non-2xx) | `ApplicationException` (contains response body) |
| Serialization issues | `JsonException` (via `ApplicationException`) |
| Cancellation requested | `OperationCanceledException` |

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](https://github.com/egov-moldova/Egov.Integrations.MNotify/blob/master/CONTRIBUTING.md) for guidelines on how to get started.

---

## Code of Conduct

This project adheres to the [Contributor Covenant Code of Conduct](https://github.com/egov-moldova/Egov.Integrations.MNotify/blob/master/CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

---

## AI Assistance

This repository contains an [AGENTS.md](https://github.com/egov-moldova/Egov.Integrations.MNotify/blob/master/AGENTS.md) file with instructions and context for AI coding agents to assist in development, ensuring consistency in code style and project structure.

---

## License

This project is licensed under the [MIT License](https://github.com/egov-moldova/Egov.Integrations.MNotify/blob/master/LICENSE).

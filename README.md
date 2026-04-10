# Egov.Integrations.MNotify

[![NuGet](https://img.shields.io/nuget/v/Egov.Integrations.MNotify.svg)](https://www.nuget.org/packages/Egov.Integrations.MNotify)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A .NET library for integrating with the MNotify service. It provides a client to send localized notifications (Email, SMS, Push, etc.), check their status, and manage notification templates. It is designed to be used in services built on the eGov platform and leverages `Egov.Extensions.Configuration` for secure certificate-based authentication (mTLS).

---

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
  - [Dependency Injection (Recommended)](#dependency-injection-recommended)
  - [MNotifyClient Usage](#mnotifyclient-usage)
  - [Template Management](#template-management)
- [Error Handling](#error-handling)
- [Testing](#testing)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [AI Assistance](#ai-assistance)
- [License](#license)

---

## Features

- **Send Notifications**: Dispatch localized notifications via various channels (Email, SMS, etc.).
- **Status Tracking**: Retrieve the current status and detailed recipient-level status of sent notifications.
- **Template Management**: Complete CRUD operations for notification templates with localized subjects and bodies.
- **Pagination Support**: List notifications and templates with sorting and searching.
- **Certificate-based Auth**: Seamless integration with `Egov.Extensions.Configuration` for mutual TLS (mTLS).
- **Localized Content**: Support for English, Romanian, and Russian content.
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

Install the package from [NuGet](https://www.nuget.org/packages/Egov.Integrations.MNotify):

```shell
dotnet add package Egov.Integrations.MNotify
```

Or via the Package Manager Console:

```shell
Install-Package Egov.Integrations.MNotify
```

---

## Configuration

Add the following sections to your **appsettings.json**:

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

The MNotify client automatically uses the certificate configured via `Egov.Extensions.Configuration`.

---

## Usage

### Dependency Injection (Recommended)

Register the certificate and the MNotify client in **Program.cs**:

```csharp
using Egov.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register the system certificate (required for mTLS)
builder.Services.AddSystemCertificate(builder.Configuration.GetSection("Certificate"));

// Register the MNotify client
builder.Services.AddMNotifyClient(builder.Configuration.GetSection("MNotify"));

var app = builder.Build();
```

### MNotifyClient Usage

Inject `IMNotifyClient` into your services to interact with MNotify:

```csharp
public class NotificationService
{
    private readonly IMNotifyClient _mNotifyClient;

    public NotificationService(IMNotifyClient mNotifyClient)
    {
        _mNotifyClient = mNotifyClient;
    }

    public async Task SendEmailAsync()
    {
        var request = new NotificationRequest
        {
            Subject = new NotificationContent { Romanian = "Bun venit la eGov", English = "Welcome to eGov" },
            Body = new NotificationContent { Romanian = "Vă mulțumim că utilizați serviciile noastre.", English = "Thank you for using our services." },
            ShortBody = new NotificationContent { Romanian = "Bun venit!", English = "Welcome!" },
            Recipients = new List<NotificationRecipient>
            {
                new NotificationRecipient { Type = NotificationRecipientType.IDNP, Value = "1234567890123" }
            }
        };

        Guid notificationId = await _mNotifyClient.SendNotificationAsync(request);
        
        // Later, check the status
        var status = await _mNotifyClient.GetNotificationStatusAsync(notificationId);
        if (status?.Status == NotificationStatus.Delivered)
        {
            // Process success
        }
    }

    public async Task ListNotificationsAsync()
    {
        var pagination = new NotificationPagination { Page = 1, ItemsPerPage = 10 };
        var notifications = await _mNotifyClient.GetNotificationsAsync(pagination);
        // ...
    }
}
```

### Template Management

Manage your notification templates directly:

```csharp
public async Task ManageTemplatesAsync(IMNotifyClient client)
{
    var newTemplate = new NotificationTemplate
    {
        Name = "WelcomeTemplate",
        Description = "Template for new users",
        Subject = new NotificationContent { Romanian = "Bun venit!" },
        ShortBody = new NotificationContent { Romanian = "Bun venit!" }
    };

    Guid templateId = await client.CreateTemplateAsync(newTemplate);
    
    var template = await client.GetTemplateAsync(templateId);
    
    // Update template
    newTemplate.Description = "Updated description";
    await client.UpdateTemplateAsync(templateId, newTemplate);
    
    // Delete template
    await client.DeleteTemplateAsync(templateId);
}
```

---

## Error Handling

The client library throws `ApplicationException` in most error scenarios, wrapping the original response or exception:

| Scenario | Exception |
|----------|-----------|
| Certificate not configured | `ApplicationException` |
| Invalid API response (non-2xx) | `ApplicationException` (contains response body) |
| Serialization issues | `JsonException` (via `ApplicationException`) |
| Cancellation requested | `OperationCanceledException` |

---

## Testing

The solution includes a test project `Egov.Integrations.MNotify.Tests` built with [xUnit](https://xunit.net/).

### Running the tests

```shell
dotnet test src/Egov.Integrations.MNotify.Tests
```

Or from the solution root:

```shell
dotnet test
```

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to get started.

---

## Code of Conduct

This project adheres to the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

---

## AI Assistance

This repository contains an [AGENTS.md](AGENTS.md) file with instructions and context for AI coding agents to assist in development, ensuring consistency in code style and project structure.

---

## License

This project is licensed under the [MIT License](LICENSE).

using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents a notification request.
/// </summary>
public class NotificationRequest
{
    /// <summary>
    /// Notification subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public required NotificationContent Subject { get; set; }

    /// <summary>
    /// Notification body.
    /// </summary>
    [JsonPropertyName("body")]
    public required NotificationContent Body { get; set; }

    /// <summary>
    /// Short notification body.
    /// </summary>
    [JsonPropertyName("bodyShort")]
    public required NotificationContent ShortBody { get; set; }

    /// <summary>
    /// Notification template to use.
    /// </summary>
    [JsonPropertyName("template")]
    public NotificationTemplateData? Template { get; set; }

    /// <summary>
    /// Notification recipients.
    /// </summary>
    [JsonPropertyName("recipients")]
    public required IList<NotificationRecipient> Recipients { get; set; }

    /// <summary>
    /// Notification priority. By default it is set to <see cref="NotificationPriority.Low"/>.
    /// </summary>
    [JsonPropertyName("priority")]
    public NotificationPriority Priority { get; set; } = NotificationPriority.Low;

    /// <summary>
    /// Notification resolution policy.
    /// </summary>
    [JsonPropertyName("resolutionPolicy")]
    public NotificationResolutionPolicy? ResolutionPolicy { get; set; }

    /// <summary>
    /// Notification attachments.
    /// </summary>
    [JsonPropertyName("attachments")]
    public IList<NotificationAttachment>? Attachments { get; set; }
}

/// <summary>
/// Represents data for notification template.
/// </summary>
public class NotificationTemplateData
{
    /// <summary>
    /// Notification template identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Notification template variables.
    /// </summary>
    [JsonPropertyName("variables")]
    public string? Variables { get; set; }
}

/// <summary>
/// Represents a notification recipient.
/// </summary>
public class NotificationRecipient
{
    /// <summary>
    /// Recipient type.
    /// </summary>
    [JsonPropertyName("type")]
    public NotificationRecipientType Type { get; set; }

    /// <summary>
    /// Recipient identifier according to the provided <see cref="Type"/>.
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; set; }
}

/// <summary>
/// Represents notification resolution policy.
/// </summary>
public class NotificationResolutionPolicy
{
    /// <summary>
    /// Identifier type to resolve to.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Resolution parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public NotificationResolutionParameters? Parameters { get; set; }
}

/// <summary>
/// Represents resolution parameters.
/// </summary>
public class NotificationResolutionParameters
{
    /// <summary>
    /// Resolution direction.
    /// </summary>
    [JsonPropertyName("direction")]
    public required string Direction { get; set; }
}

/// <summary>
/// Represents a notification attachment.
/// </summary>
public class NotificationAttachment
{
    /// <summary>
    /// Attachment file name.
    /// </summary>
    [JsonPropertyName("fileName")]
    public required string FileName { get; set; }

    /// <summary>
    /// Attachment content.
    /// </summary>
    [JsonPropertyName("base64")]
    public required string Base64Content { get; set; }
}

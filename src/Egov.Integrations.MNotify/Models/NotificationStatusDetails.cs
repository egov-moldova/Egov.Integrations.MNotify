using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents details on notification status.
/// </summary>
public class NotificationStatusDetails
{
    /// <summary>
    /// Notification identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Notification status.
    /// </summary>
    [JsonPropertyName("status")]
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Actual notification recipients.
    /// </summary>
    [JsonPropertyName("recipients")]
    public required IList<NotificationStatusRecipient> Recipients { get; set; } 
}

/// <summary>
/// Represents a notification recipient.
/// </summary>
public class NotificationStatusRecipient : NotificationEntity
{
    /// <summary>
    /// Recipient type.
    /// </summary>
    [JsonPropertyName("type")]
    public NotificationRecipientType Type { get; set; }

    /// <summary>
    /// Actual recipient identifier.
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; set; }

    /// <summary>
    /// Recipient messages.
    /// </summary>
    [JsonPropertyName("messages")]
    public required IList<NotificationMessage> Messages { get; set; }
}

/// <summary>
/// Represents a notification message.
/// </summary>
public class NotificationMessage : NotificationEntity
{
    /// <summary>
    /// Notification message identifier.
    /// </summary>
    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }

    /// <summary>
    /// Notification status.
    /// </summary>
    [JsonPropertyName("status")]
    public NotificationStatus Status { get; set; }

    /// <summary>
    /// Channel used for the message.
    /// </summary>
    [JsonPropertyName("channel")]
    public required string Channel { get; set; }

    /// <summary>
    /// Message subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public required string Subject { get; set; }
}

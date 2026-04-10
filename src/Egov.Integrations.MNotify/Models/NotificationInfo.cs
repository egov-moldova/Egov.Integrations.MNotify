using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Notification information.
/// </summary>
public class NotificationInfo : NotificationEntity
{
    /// <summary>
    /// Notification ID.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Notification status.
    /// </summary>
    [JsonPropertyName("status")]
    public NotificationStatus Status { get; set; }
}

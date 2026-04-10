using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Notification status.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationStatus
{
    /// <summary>
    /// Notification is pending.
    /// </summary>
    Pending,
    
    /// <summary>
    /// Notification is resolving.
    /// </summary>
    Resolving,

    /// <summary>
    /// Notification is being sent.
    /// </summary>
    Sending,

    /// <summary>
    /// Notification was sent.
    /// </summary>
    Sent,

    /// <summary>
    /// Notification was delivered.
    /// </summary>
    Delivered,

    /// <summary>
    /// Notification was read.
    /// </summary>
    Read,

    /// <summary>
    /// Notification is being cancelled.
    /// </summary>
    Cancelling,

    /// <summary>
    /// Notification was cancelled.
    /// </summary>
    Cancelled,

    /// <summary>
    /// Notification failed.
    /// </summary>
    Failed
}

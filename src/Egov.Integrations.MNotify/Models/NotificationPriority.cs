using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Notification priority.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationPriority
{
    /// <summary>
    /// Low notification priority.
    /// </summary>
    Low,

    /// <summary>
    /// Medium notification priority.
    /// </summary>
    Medium,

    /// <summary>
    /// High notification priority.
    /// </summary>
    High
}

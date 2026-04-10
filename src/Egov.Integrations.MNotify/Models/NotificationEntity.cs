using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// A base class used for various notification entities.
/// </summary>
public class NotificationEntity
{
    /// <summary>
    /// Entity creation date.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Entity last modification date.
    /// </summary>
    [JsonPropertyName("lastUpdatedAt")]
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>
    /// Information about entity creator.
    /// </summary>
    [JsonPropertyName("createdBy")]
    public required string CreatedBy { get; set; }

    /// <summary>
    /// Information about entity modifier.
    /// </summary>
    [JsonPropertyName("lastUpdatedBy")]
    public required string LastUpdatedBy { get; set; }
}
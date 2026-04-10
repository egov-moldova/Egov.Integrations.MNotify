using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents details about notification template.
/// </summary>
public class NotificationTemplateDetails : NotificationTemplate
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Template sender identifier.
    /// </summary>
    [JsonPropertyName("senderId")]
    public string? SenderId { get; set; }

    /// <summary>
    /// The date this template was created at.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date this template was last updated at.
    /// </summary>
    [JsonPropertyName("lastUpdatedAt")]
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>
    /// Information about template creator.
    /// </summary>
    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Information about template modifier.
    /// </summary>
    [JsonPropertyName("lastUpdatedBy")]
    public string? LastUpdatedBy { get; set; }
}

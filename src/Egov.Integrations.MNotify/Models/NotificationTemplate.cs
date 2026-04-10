using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents a notification template.
/// </summary>
public class NotificationTemplate
{
    /// <summary>
    /// Template name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Template description.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Templated notification subject.
    /// </summary>
    [JsonPropertyName("subject")]
    public required NotificationContent Subject { get; set; }

    /// <summary>
    /// Templated notification body.
    /// </summary>
    [JsonPropertyName("body")]
    public NotificationContent? Body { get; set; }

    /// <summary>
    /// Templated short notification body.
    /// </summary>
    [JsonPropertyName("bodyShort")]
    public required NotificationContent ShortBody { get; set; }
}

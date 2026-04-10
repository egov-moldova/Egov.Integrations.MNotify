using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Template information.
/// </summary>
public class NotificationTemplateInfo : NotificationEntity
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Template name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

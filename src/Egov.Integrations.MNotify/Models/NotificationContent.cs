using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Localized notification content.
/// </summary>
public class NotificationContent
{
    /// <summary>
    /// English notification content.
    /// </summary>
    [JsonPropertyName("en")]
    public string? English { get; set; }

    /// <summary>
    /// Romanian notification content.
    /// </summary>
    [JsonPropertyName("ro")]
    public required string Romanian { get; set; }

    /// <summary>
    /// Russian notification content.
    /// </summary>
    [JsonPropertyName("ru")]
    public string? Russian { get; set; }
}

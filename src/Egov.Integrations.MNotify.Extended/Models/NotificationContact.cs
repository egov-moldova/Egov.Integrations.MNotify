using Egov.Integrations.MNotify.Models;
using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Extended.Models;

/// <summary>
/// Represents a notification contact.
/// </summary>
public class NotificationContact
{
    /// <summary>
    /// Actual contact value.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }

    /// <summary>
    /// Contact channel.
    /// </summary>
    [JsonPropertyName("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// Contact preferences.
    /// </summary>
    [JsonPropertyName("preferences")]
    public string Preferences { get; set; }

    /// <summary>
    /// Contact device name.
    /// </summary>
    [JsonPropertyName("deviceName")]
    public string DeviceName { get; set; }

    /// <summary>
    /// Contact recipient.
    /// </summary>
    [JsonPropertyName("recipient")]
    public string Recipient { get; set; }

    /// <summary>
    /// Contact recipient type.
    /// </summary>
    [JsonPropertyName("recipientType")]
    public NotificationRecipientType RecipientType { get; set; }

    /// <summary>
    /// Contact recipient first name.
    /// </summary>
    [JsonPropertyName("recipientFirstName")]
    public string RecipientFirstName { get; set; }

    /// <summary>
    /// Contact recipient last name.
    /// </summary>
    [JsonPropertyName("recipientLastName")]
    public string RecipientLastName { get; set; }
}

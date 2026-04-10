using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Notification recipient type.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationRecipientType
{
    /// <summary>
    /// Recipient is a physical person by IDNP.
    /// </summary>
    IDNP,

    /// <summary>
    /// Recipient is a legal entity by IDNO.
    /// </summary>
    IDNO,

    /// <summary>
    /// Recipient is an e-mail address.
    /// </summary>
    Email,

    /// <summary>
    /// Recipient is a phone number.
    /// </summary>
    Telephone
}

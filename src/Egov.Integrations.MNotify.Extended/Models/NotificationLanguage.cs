using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Extended.Models;

/// <summary>
/// Represents recipient language.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationLanguage
{
    /// <summary>
    /// Represents Romanian language.
    /// </summary>
    Ro = 1,

    /// <summary>
    /// Represents English language.
    /// </summary>
    En = 2,

    /// <summary>
    /// Represents Russian language.
    /// </summary>
    Ru = 3
}

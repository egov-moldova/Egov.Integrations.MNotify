using System.Text.Json.Serialization;
using Egov.Integrations.MNotify.Models;

namespace Egov.Integrations.MNotify.Extended.Models;

public class ContactInfo : NotificationEntity
{
    [JsonPropertyName("contact")]
    public string Contact { get; set; }

    [JsonPropertyName("contactType")]
    public string ContactType { get; set; }

    [JsonPropertyName("verification")]
    public bool Verification { get; set; }
}

using System.Text.Json.Serialization;
using Egov.Integrations.MNotify.Models;

namespace Egov.Integrations.MNotify.Extended.Models;

public class NotificationMessageInfo : NotificationEntity
{
    [JsonPropertyName("messageId")]
    public Guid MessageId { get; set; }

    [JsonPropertyName("senderIDNO")]
    public string SenderIDNO { get; set; }

    [JsonPropertyName("serviceName")]
    public string ServiceName { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("read")]
    public bool Read { get; set; }

    [JsonPropertyName("hasAttachments")]
    public bool HasAttachments { get; set; }
}

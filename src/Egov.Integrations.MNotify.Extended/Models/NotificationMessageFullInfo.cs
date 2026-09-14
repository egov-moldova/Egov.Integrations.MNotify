using System.Text.Json.Serialization;
using Egov.Integrations.MNotify.Models;

namespace Egov.Integrations.MNotify.Extended.Models;

public class NotificationMessagesFullInfo
{
    [JsonPropertyName("Items")]
    public List<NotificationMessageInfo> Items { get; set; }

    [JsonPropertyName("PagedSummary")]
    public NotificationPaginationSummary PagedSummary { get; set; }
}

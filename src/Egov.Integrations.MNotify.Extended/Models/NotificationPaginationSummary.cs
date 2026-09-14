using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Extended.Models;

public class NotificationPaginationSummary
{
    [JsonPropertyName("TotalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("PageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("CurrentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("TotalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("UnreadMessages")]
    public int UnreadMessages { get; set; }
}

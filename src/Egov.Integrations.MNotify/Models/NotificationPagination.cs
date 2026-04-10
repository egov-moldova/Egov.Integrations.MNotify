using System.Text.Json.Serialization;

namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents pagination parameters.
/// </summary>
public class NotificationPagination
{
    /// <summary>
    /// Page to return.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// Items per page to return.
    /// </summary>
    [JsonPropertyName("itemsPerPage")]
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Field to sort by.
    /// </summary>
    [JsonPropertyName("orderField")]
    public string? OrderField { get; set; }

    /// <summary>
    /// Search by parameter.
    /// </summary>
    [JsonPropertyName("searchBy")]
    public string? SearchBy { get; set; }
}

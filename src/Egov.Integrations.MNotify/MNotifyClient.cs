using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Egov.Integrations.MNotify.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace Egov.Integrations.MNotify;

internal class MNotifyClient(HttpClient httpClient) : IMNotifyClient
{
    internal static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    protected readonly HttpClient HttpClient = httpClient;

    #region Notification

    public async Task<IList<NotificationInfo>?> GetNotificationsAsync(NotificationPagination? pagination = null, CancellationToken cancellationToken = default)
    {
        var uri = PreparePaginationUri("/api/notification", pagination);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<List<NotificationInfo>>(responseString, JsonSerializerOptions);
    }

    public async Task<Guid> SendNotificationAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken = default)
    {
        var uri = "/api/notification";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PostAsJsonAsync(uri, notificationRequest, JsonSerializerOptions, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<Guid>(responseString, JsonSerializerOptions);
    }

    public async Task<NotificationStatusDetails?> GetNotificationStatusAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/notification/{notificationId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<NotificationStatusDetails>(responseString, JsonSerializerOptions);
    }

    public async Task CancelNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/notification/{notificationId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.DeleteAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }
    }

    #endregion

    #region Template

    public async Task<IList<NotificationTemplateInfo>?> GetTemplatesAsync(NotificationPagination? pagination = null, CancellationToken cancellationToken = default)
    {
        var uri = PreparePaginationUri("/api/template", pagination);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<List<NotificationTemplateInfo>>(responseString, JsonSerializerOptions);
    }

    public async Task<Guid> CreateTemplateAsync(NotificationTemplate template, CancellationToken cancellationToken = default)
    {
        var uri = "/api/template";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PostAsJsonAsync(uri, template, JsonSerializerOptions, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<Guid>(responseString, JsonSerializerOptions);
    }

    public async Task<NotificationTemplateDetails?> GetTemplateAsync(Guid templateId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/template/{templateId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<NotificationTemplateDetails>(responseString, JsonSerializerOptions);
    }

    public async Task<Guid> UpdateTemplateAsync(Guid templateId, NotificationTemplate template, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/template/{templateId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PutAsJsonAsync(uri, template, JsonSerializerOptions, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }

        return JsonSerializer.Deserialize<Guid>(responseString, JsonSerializerOptions);
    }

    public async Task DeleteTemplateAsync(Guid templateId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/template/{templateId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.DeleteAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new ApplicationException(responseString, e);
        }
    }

    #endregion

    internal static string PreparePaginationUri(string uri, NotificationPagination? pagination)
    {
        if (pagination == null) return uri;
        if (pagination.Page != default)
            uri = QueryHelpers.AddQueryString(uri, nameof(pagination.Page), pagination.Page.ToString());
        if (pagination.ItemsPerPage != default)
            uri = QueryHelpers.AddQueryString(uri, nameof(pagination.ItemsPerPage), pagination.ItemsPerPage.ToString());
        if (!string.IsNullOrWhiteSpace(pagination.OrderField))
            uri = QueryHelpers.AddQueryString(uri, nameof(pagination.OrderField), pagination.OrderField);
        if (!string.IsNullOrWhiteSpace(pagination.SearchBy))
            uri = QueryHelpers.AddQueryString(uri, nameof(pagination.SearchBy), pagination.SearchBy);

        return uri;
    }
}

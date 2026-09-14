using System.Net.Http.Json;
using System.Text.Json;
using Egov.Integrations.MNotify.Extended.Models;
using Egov.Integrations.MNotify.Models;

namespace Egov.Integrations.MNotify.Extended;

internal class MNotifyExtendedClient : MNotifyClient, IMNotifyExtendedClient
{
    public MNotifyExtendedClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<IList<NotificationMessageInfo>?> GetNotificationMessagesAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default)
    {
        var uri = PreparePaginationUri($"/api/recipient/{userId}/messages", pagination);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }

        return JsonSerializer.Deserialize<List<NotificationMessageInfo>>(responseString, JsonSerializerOptions);
    }

    public async Task<NotificationMessagesFullInfo?> GetNotificationMessagesFullInfoAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default)
    {
        var uri = PreparePaginationUri($"/api/recipient/{userId}/messages/full", pagination);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }

        return JsonSerializer.Deserialize<NotificationMessagesFullInfo>(responseString, JsonSerializerOptions);
    }

    public async Task<Models.NotificationMessage?> GetNotificationMessageAsync(string userId, Guid messageId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/recipient/{userId}/messages/{messageId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }

        return JsonSerializer.Deserialize<Models.NotificationMessage>(responseString, JsonSerializerOptions);
    }

    public async Task UpdateMessageReadFlagAsync(string userId, Guid messageId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/recipient/{userId}/messages/{messageId}";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PatchAsync(uri, null, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }
    }

    public async Task MarkAllMessagesAsRead(string userId, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/recipient/{userId}/messages/mark-as-read";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PatchAsync(uri, null, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }
    }

    public async Task<IList<ContactInfo>?> GetContactsAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default)
    {
        var uri = PreparePaginationUri($"/api/recipient/{userId}/contact", pagination);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.GetAsync(uri, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }

        return JsonSerializer.Deserialize<List<ContactInfo>>(responseString, JsonSerializerOptions);
    }

    public async Task<int> CreateContactAsync(NotificationContact contact, CancellationToken cancellationToken = default)
    {
        var uri = "/api/recipient/contact";

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PostAsJsonAsync(uri, contact, JsonSerializerOptions, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }

        return JsonSerializer.Deserialize<int>(responseString, JsonSerializerOptions);
    }

    public async Task EditLanguageAsync(string userId, NotificationLanguage language, CancellationToken cancellationToken = default)
    {
        var uri = $"/api/recipient/{userId}/language";

        var requestJson = JsonContent.Create(new { language }, options: JsonSerializerOptions);

        var responseString = string.Empty;
        try
        {
            using var response = await HttpClient.PatchAsync(uri, requestJson, cancellationToken);
            responseString = await response.Content.ReadAsStringAsync(cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(responseString, ex);
        }
    }
}
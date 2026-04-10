namespace Egov.Integrations.MNotify.Models;

/// <summary>
/// Represents the interface of MNotify client.
/// </summary>
public interface IMNotifyClient
{
    /// <summary>
    /// Retrieves notifications sent by this client.
    /// </summary>
    /// <param name="pagination">Pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A list of information about notifications.</returns>
    Task<IList<NotificationInfo>?> GetNotificationsAsync(NotificationPagination? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a notification.
    /// </summary>
    /// <remarks>Note that Romanian property in ContentLanguage object is mandatory </remarks>
    /// <param name="notificationRequest">A structure that describes the notification request.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>The identifier of the notification sent.</returns>
    Task<Guid> SendNotificationAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves notification status.
    /// </summary>
    /// <param name="notificationId">The ID of the notification.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A structure that represents notification status</returns>
    Task<NotificationStatusDetails?> GetNotificationStatusAsync(Guid notificationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Tries to cancel a pending notification.
    /// </summary>
    /// <param name="notificationId">The ID of the notification.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    Task CancelNotificationAsync(Guid notificationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves templates created by this client.
    /// </summary>
    /// <param name="pagination">Pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A list of template details</returns>
    Task<IList<NotificationTemplateInfo>?> GetTemplatesAsync(NotificationPagination? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new template.
    /// </summary>
    /// <param name="template">Template parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>Newly created template ID</returns>
    Task<Guid> CreateTemplateAsync(NotificationTemplate template, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves template by its identifier.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A structure that represents template</returns>
    Task<NotificationTemplateDetails?> GetTemplateAsync(Guid templateId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the existing template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="template">Template parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>Updated template identifier</returns>
    Task<Guid> UpdateTemplateAsync(Guid templateId, NotificationTemplate template, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the existing template.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    Task DeleteTemplateAsync(Guid templateId, CancellationToken cancellationToken = default);
}

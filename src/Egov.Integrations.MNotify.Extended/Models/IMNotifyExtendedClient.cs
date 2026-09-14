using Egov.Integrations.MNotify.Models;

namespace Egov.Integrations.MNotify.Extended.Models;

/// <summary>
/// Represents an interface of extended MNotify client.
/// </summary>
public interface IMNotifyExtendedClient : IMNotifyClient
{
    /// <summary>
    /// Retrieves paginated notification messages with summary information for a specific user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="pagination">Pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>Notification messages with paging summary.</returns>
    Task<IList<NotificationMessageInfo>?> GetNotificationMessagesAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves notifications messages sent by a specific user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="pagination">Pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A list of information about notifications messages.</returns>
    Task<NotificationMessagesFullInfo?> GetNotificationMessagesFullInfoAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific notification message.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="messageId">Message identifier</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A notification message</returns>
    Task<NotificationMessage?> GetNotificationMessageAsync(string userId, Guid messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the read flag of a notification message
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="messageId">Message identifier</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    Task UpdateMessageReadFlagAsync(string userId, Guid messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Mark all messages as read of provided user id
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    Task MarkAllMessagesAsRead(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves contacts of a specific user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="pagination">Pagination parameters.</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>A list of contacts information</returns>
    Task<IList<ContactInfo>?> GetContactsAsync(string userId, NotificationPagination? pagination = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new contact
    /// </summary>
    /// <param name="contact">Contact model object</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    /// <returns>Newly created contact identifier</returns>
    Task<int> CreateContactAsync(NotificationContact contact, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates user language.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="language">Two letter language</param>
    /// <param name="cancellationToken">A cancellation token to observe.</param>
    Task EditLanguageAsync(string userId, NotificationLanguage language, CancellationToken cancellationToken = default);
}
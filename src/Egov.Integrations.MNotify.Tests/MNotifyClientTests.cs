using System.Net;
using System.Text.Json;
using Egov.Integrations.MNotify.Models;
using Moq;
using Moq.Protected;
using Xunit;

namespace Egov.Integrations.MNotify.Tests;

public class MNotifyClientTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _httpClient;
    private readonly MNotifyClient _client;

    public MNotifyClientTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_handlerMock.Object)
        {
            BaseAddress = new Uri("https://api.mnotify.test")
        };
        _client = new MNotifyClient(_httpClient);
    }

    [Fact]
    public async Task SendNotificationAsync_ShouldReturnGuid_WhenSuccessful()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var request = CreateValidRequest();
        
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Post && 
                    req.RequestUri!.AbsolutePath == "/api/notification"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedId, MNotifyClient.JsonSerializerOptions))
            });

        // Act
        var result = await _client.SendNotificationAsync(request);

        // Assert
        Assert.Equal(expectedId, result);
    }

    [Fact]
    public async Task GetNotificationStatusAsync_ShouldReturnDetails_WhenSuccessful()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        var expectedDetails = new NotificationStatusDetails 
        { 
            Id = notificationId,
            Recipients = new List<NotificationStatusRecipient>()
        };
        
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && 
                    req.RequestUri!.AbsolutePath == $"/api/notification/{notificationId}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedDetails, MNotifyClient.JsonSerializerOptions))
            });

        // Act
        var result = await _client.GetNotificationStatusAsync(notificationId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(notificationId, result.Id);
    }

    [Fact]
    public async Task CancelNotificationAsync_ShouldSucceed_WhenSuccessful()
    {
        // Arrange
        var notificationId = Guid.NewGuid();
        
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Delete && 
                    req.RequestUri!.AbsolutePath == $"/api/notification/{notificationId}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        // Act & Assert
        await _client.CancelNotificationAsync(notificationId);
    }

    [Fact]
    public async Task CreateTemplateAsync_ShouldReturnGuid_WhenSuccessful()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var template = new NotificationTemplate
        {
            Name = "Test Template",
            Description = "Test Description",
            Subject = new NotificationContent { Romanian = "Subject" },
            ShortBody = new NotificationContent { Romanian = "Short Body" }
        };
        
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Post && 
                    req.RequestUri!.AbsolutePath == "/api/template"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedId, MNotifyClient.JsonSerializerOptions))
            });

        // Act
        var result = await _client.CreateTemplateAsync(template);

        // Assert
        Assert.Equal(expectedId, result);
    }

    [Fact]
    public async Task GetTemplatesAsync_ShouldReturnList_WhenSuccessful()
    {
        // Arrange
        var expectedTemplates = new List<NotificationTemplateInfo>
        {
            new NotificationTemplateInfo 
            { 
                Id = Guid.NewGuid(), 
                Name = "Template 1",
                CreatedBy = "System",
                LastUpdatedBy = "System"
            }
        };

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Get && 
                    req.RequestUri!.AbsolutePath == "/api/template"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedTemplates, MNotifyClient.JsonSerializerOptions))
            });

        // Act
        var result = await _client.GetTemplatesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expectedTemplates[0].Id, result[0].Id);
    }

    [Fact]
    public async Task UpdateTemplateAsync_ShouldReturnGuid_WhenSuccessful()
    {
        // Arrange
        var templateId = Guid.NewGuid();
        var template = new NotificationTemplate
        {
            Name = "Updated Name",
            Description = "Updated Description",
            Subject = new NotificationContent { Romanian = "Updated Subject" },
            ShortBody = new NotificationContent { Romanian = "Updated Short Body" }
        };

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Put && 
                    req.RequestUri!.AbsolutePath == $"/api/template/{templateId}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(templateId, MNotifyClient.JsonSerializerOptions))
            });

        // Act
        var result = await _client.UpdateTemplateAsync(templateId, template);

        // Assert
        Assert.Equal(templateId, result);
    }

    [Fact]
    public async Task DeleteTemplateAsync_ShouldSucceed_WhenSuccessful()
    {
        // Arrange
        var templateId = Guid.NewGuid();

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => 
                    req.Method == HttpMethod.Delete && 
                    req.RequestUri!.AbsolutePath == $"/api/template/{templateId}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        // Act & Assert
        await _client.DeleteTemplateAsync(templateId);
    }

    [Fact]
    public async Task SendNotificationAsync_ShouldThrowApplicationException_WhenApiFails()
    {
        // Arrange
        var request = CreateValidRequest();
        var errorContent = "Invalid request";
        
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorContent)
            });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => _client.SendNotificationAsync(request));
        Assert.Contains(errorContent, exception.Message);
    }

    [Fact]
    public void PreparePaginationUri_ShouldAppendQueryParameters()
    {
        // Arrange
        var baseUri = "/api/test";
        var pagination = new NotificationPagination
        {
            Page = 2,
            ItemsPerPage = 50,
            OrderField = "Name",
            SearchBy = "Term"
        };

        // Act
        var result = MNotifyClient.PreparePaginationUri(baseUri, pagination);

        // Assert
        Assert.Contains("Page=2", result);
        Assert.Contains("ItemsPerPage=50", result);
        Assert.Contains("OrderField=Name", result);
        Assert.Contains("SearchBy=Term", result);
    }

    private static NotificationRequest CreateValidRequest()
    {
        return new NotificationRequest
        {
            Subject = new NotificationContent { Romanian = "Subject" },
            Body = new NotificationContent { Romanian = "Body" },
            ShortBody = new NotificationContent { Romanian = "Short Body" },
            Recipients = new List<NotificationRecipient>
            {
                new NotificationRecipient { Type = NotificationRecipientType.IDNP, Value = "1234567890123" }
            }
        };
    }
}

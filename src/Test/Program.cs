using Egov.Integrations.MNotify.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSystemCertificate(builder.Configuration.GetSection("Certificate"));
builder.Services.AddMNotifyClient(builder.Configuration.GetSection("MNotify"));

var app = builder.Build();

app.MapGet("/", async (IMNotifyClient client) =>
{
    #region Notification Test
    var notificationId = await client.SendNotificationAsync(new NotificationRequest
    {
        Subject = new NotificationContent
        {
            Romanian = "test"
        },
        Body = new NotificationContent
        {
            Romanian = "testare"
        },
        ShortBody = new NotificationContent
        {
            Romanian = "testare"
        },
        Recipients = new List<NotificationRecipient>
                    {
                        new NotificationRecipient
                        {
                            Type = NotificationRecipientType.IDNP,
                            Value = "11111111111111"
                        }
                    }
    });

    var notification = await client.GetNotificationStatusAsync(notificationId);
    #endregion
});

app.Run();
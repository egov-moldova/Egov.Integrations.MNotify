using System.Security.Cryptography.X509Certificates;
using Egov.Extensions.Configuration;
using Egov.Integrations.MNotify.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Egov.Integrations.MNotify.Tests;

public class MNotifyClientExtensionsTests
{
    [Fact]
    public void AddMNotifyClient_ShouldRegisterServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["MNotify:BaseAddress"] = "https://api.mnotify.test"
            })
            .Build();

        // Register system certificate from path
        services.AddSystemCertificate(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Certificate:Path"] = "cert" })
            .Build().GetSection("Certificate"));

        // Act
        services.AddMNotifyClient(configuration.GetSection("MNotify"));
        var provider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(provider.GetService<IMNotifyClient>());
        var options = provider.GetRequiredService<IOptions<MNotifyClientOptions>>().Value;
        Assert.Equal("https://api.mnotify.test/", options.BaseAddress.ToString());
    }

    [Fact]
    public void AddMNotifyClient_ShouldPropagateSystemCertificate()
    {
        // Arrange
        var services = new ServiceCollection();
        // Register system certificate from path
        var configurationForCert = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Certificate:Path"] = "cert" })
            .Build().GetSection("Certificate");
        services.AddSystemCertificate(configurationForCert);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["MNotify:BaseAddress"] = "https://api.mnotify.test"
            })
            .Build();

        // Act
        services.AddMNotifyClient(configuration.GetSection("MNotify"));
        var provider = services.BuildServiceProvider();

        // Triggering the configuration of MNotifyClientOptions
        var mnotifyOptions = provider.GetRequiredService<IOptions<MNotifyClientOptions>>().Value;

        // Assert
        Assert.NotNull(mnotifyOptions.SystemCertificate);
        Assert.True(mnotifyOptions.SystemCertificate.HasPrivateKey);
    }

    [Fact]
    public void AddMNotifyClient_ShouldWorkWithDirectoryCertificate()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["MNotify:BaseAddress"] = "https://api.mnotify.test",
                ["Certificate:Path"] = "cert"
            })
            .Build();

        // Register system certificate from path
        services.AddSystemCertificate(configuration.GetSection("Certificate"));

        // Act
        services.AddMNotifyClient(configuration.GetSection("MNotify"));
        var provider = services.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<MNotifyClientOptions>>().Value;
        Assert.NotNull(options.SystemCertificate);
        Assert.True(options.SystemCertificate.HasPrivateKey);
    }

}

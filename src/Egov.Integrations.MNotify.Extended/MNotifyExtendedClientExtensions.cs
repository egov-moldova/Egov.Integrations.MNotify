using System.Net.Security;
using Egov.Extensions.Configuration;
using Egov.Integrations.MNotify;
using Egov.Integrations.MNotify.Extended;
using Egov.Integrations.MNotify.Extended.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions methods to add extended MNotify client to an application.
/// </summary>
public static class MNotifyExtendedClientExtensions
{
    /// <summary>
    /// Adds MNotify extended client implementation.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/> to add the client to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMNotifyExtendedClient(this IServiceCollection services)
        => services.AddMNotifyExtendedClient(_ => { });

    /// <summary>
    /// Adds MNotify extended client implementation.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/> to add the client to.</param>
    /// <param name="config">The configuration being bound to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMNotifyExtendedClient(this IServiceCollection services, IConfiguration config)
        => services.AddMNotifyExtendedClient(config, _ => { });

    /// <summary>
    /// Adds MNotify extended client implementation.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/> to add the client to.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="MNotifyClientOptions"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMNotifyExtendedClient(this IServiceCollection services, Action<MNotifyClientOptions> configureOptions)
    {
        services.Configure(configureOptions);

        services.TryAddSingleton<IPostConfigureOptions<MNotifyClientOptions>, MNotifyPostConfigureOptions>();
        services.AddOptions<MNotifyClientOptions>()
            .Configure<IOptions<SystemCertificateOptions>>((mnotifyOptions, systemCertificateOptions) =>
            {
                var systemCertificateOptionsValue = systemCertificateOptions.Value;
                mnotifyOptions.SystemCertificate ??= systemCertificateOptionsValue.Certificate;
                mnotifyOptions.SystemCertificateIntermediaries ??= systemCertificateOptionsValue.IntermediateCertificates;
            });

        services.AddHttpClient<IMNotifyExtendedClient, MNotifyExtendedClient>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<MNotifyClientOptions>>().Value;
                client.BaseAddress = options.BaseAddress;
            })
            .ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var options = provider.GetRequiredService<IOptions<MNotifyClientOptions>>().Value;
                return new SocketsHttpHandler
                {
                    SslOptions = new SslClientAuthenticationOptions
                    {
                        ClientCertificateContext = SslStreamCertificateContext.Create(options.SystemCertificate!, options.SystemCertificateIntermediaries, true)
                    }
                };
            });

        return services;
    }

    /// <summary>
    /// Adds MNotify extended client implementation.
    /// </summary>
    /// <param name="services">An instance of <see cref="IServiceCollection"/> to add the client to.</param>
    /// <param name="config">The configuration being bound to.</param>
    /// <param name="configureOptions">A delegate to configure <see cref="MNotifyClientOptions"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddMNotifyExtendedClient(this IServiceCollection services, IConfiguration config, Action<MNotifyClientOptions> configureOptions)
    {
        services.Configure<MNotifyClientOptions>(config);
        return services.AddMNotifyExtendedClient(configureOptions);
    }
}

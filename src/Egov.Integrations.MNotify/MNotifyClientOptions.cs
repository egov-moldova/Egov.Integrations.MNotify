using System.Security.Cryptography.X509Certificates;

namespace Egov.Integrations.MNotify;

/// <summary>
/// Options for MNotify client.
/// </summary>
public class MNotifyClientOptions
{
    /// <summary>
    /// The base address of MNotify service.
    /// </summary>
    public required Uri BaseAddress { get; set; }

    /// <summary>
    /// Directly set the service certificate to use.
    /// </summary>
    public X509Certificate2? SystemCertificate { get; set; }

    /// <summary>
    /// Directly set the intermediate service certificates to use.
    /// </summary>
    public X509Certificate2Collection? SystemCertificateIntermediaries { get; set; }
}

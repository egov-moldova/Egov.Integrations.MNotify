using Microsoft.Extensions.Options;

namespace Egov.Integrations.MNotify;

internal class MNotifyPostConfigureOptions : IPostConfigureOptions<MNotifyClientOptions>
{
    public void PostConfigure(string? name, MNotifyClientOptions settings)
    {
        if (settings.SystemCertificate == null)
        {
            throw new ApplicationException("MNotify service certificate not configured or not available");
        }
    }
}

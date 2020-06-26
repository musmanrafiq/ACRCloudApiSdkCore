using AcrCloudApiSdk.Models;
using System.Collections.Generic;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        List<AcrChannelModel> GetArcChannels();
    }
}

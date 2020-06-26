using AcrCloudApiSdk.Models;
using System.Threading.Tasks;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        Task<ChannelResponseModel> GetArcChannelsAsync();
    }
}

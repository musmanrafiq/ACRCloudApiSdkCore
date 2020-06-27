using System.Threading.Tasks;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        Task<string> GetArcChannelsAsync();
    }
}

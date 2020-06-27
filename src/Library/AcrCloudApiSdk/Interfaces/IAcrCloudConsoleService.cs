using System.Threading.Tasks;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        Task<string> GetChannelsAsync();
        Task<string> GetProjectsAsync();
    }
}

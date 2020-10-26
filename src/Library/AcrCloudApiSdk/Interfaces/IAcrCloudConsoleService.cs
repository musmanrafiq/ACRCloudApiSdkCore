using AcrCloudApiSdk.Models;
using System.Threading.Tasks;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        Task<ChannelResponseModel> GetChannelsAsync();
        Task<ProjectResponseModel> GetProjectsAsync();
        Task<AcrUploadResponse> Upload(string audioId, string audioTitle, string filePath, string fileType);
        Task<bool> Delete(string acrId);
    }
}

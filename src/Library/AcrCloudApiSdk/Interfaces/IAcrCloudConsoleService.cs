using AcrCloudApiSdk.Models;
using System.IO;
using System.Threading.Tasks;

namespace AcrCloudApiSdk.Interfaces
{
    public interface IAcrCloudConsoleService
    {
        string GenerateFingerPrint(string filePath);
        Task<ChannelResponseModel> GetChannelsAsync();
        Task<ProjectResponseModel> GetProjectsAsync();
        Task<AcrUploadResponse> UploadFileToBucket(string audioId, string audioTitle, string filePath, string fileType);
        Task<bool> Delete(string acrId);
        Task<(bool, string, Stream)> GetRecording(string channelId, string recordTimeStamp, int playedDuration);
    }
}

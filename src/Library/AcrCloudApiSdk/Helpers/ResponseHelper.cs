using AcrCloudApiSdk.Models;

namespace AcrCloudApiSdk.Helpers
{
    public static class ResponseHelper
    {
        public static ChannelResponseModel Transform(this string jsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelResponseModel>(jsonResponse);
        }
    }
}

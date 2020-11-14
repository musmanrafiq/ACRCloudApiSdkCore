using AcrCloudApiSdk.Models;

namespace AcrCloudApiSdk.Helpers
{
    public static class ResponseHelper
    {
        public static ChannelResponseModel Transform(this string jsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelResponseModel>(jsonResponse);
        }

        public class FingerprintProcessResponse : ProcessResponse
        {
            public string FingerprintFilePath { get; set; }
        }

        public abstract class ProcessResponse
        {
            public int ExitCode { get; set; }
            public string ErrMsg { get; set; }
            public string OutputMsg { get; set; }
        }
    }
}

namespace AcrCloudApiSdk.Models.Options
{
    public class AcrCloudOptions
    {
        public string BaseUrl { get; set; }
        public string BucketName { get; set; }
        public DatabaseMonitoring DatabaseMonitoring { get; set; }
        // seconds
        public int ReportFetchInterval { get; set; }
        public string AccountAccessKey { get; set; }
        public string AccountAccessSecret { get; set; }
    }

    public class DatabaseMonitoring
    {
        public string AccessKey { get; set; }
        public string ProjectName { get; set; }
        public int ChannelPerPage { get; set; }
    }
}

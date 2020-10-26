namespace AcrCloudApiSdk.Helpers
{
    public static class HeadersKeys
    {
        public const string AccessKey = "access-key";
        public const string Signature = "signature";
        public const string signatureVersion = "signature-version";
        public const string Timestamp = "timestamp";
    }

    public static class AudioKeys
    {
        public const string AudioFileParm = "audio_file";
        public const string AudioIdParam = "audio_id";
        public const string BucketNameParam = "bucket_name";
        public const string DatatypeParam = "data_type";
        public const string TitleParam = "title";
    }

    public static class AudioEndpoint
    {
        public const string UrlPrepend = "audios";
        public const string HttpAction = "/v1/audios";
    }
}

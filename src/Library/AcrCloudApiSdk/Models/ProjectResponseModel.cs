using Newtonsoft.Json;
using System;

namespace AcrCloudApiSdk.Models
{
    public partial class ProjectResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("can_edit_url")]
        public bool CanEditUrl { get; set; }

        [JsonProperty("buckets")]
        public Bucket[] Buckets { get; set; }

        [JsonProperty("timemap")]
        public Timemap Timemap { get; set; }

        [JsonProperty("callback")]
        public Callback Callback { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }

    public partial class Bucket
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Callback
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("state")]
        public Result State { get; set; }
    }

    public partial class Result
    {
    }

    public partial class Timemap
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("lifecycle")]
        public long Lifecycle { get; set; }

        [JsonProperty("enabled_at")]
        public string EnabledAt { get; set; }
    }
}

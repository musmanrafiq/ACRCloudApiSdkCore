using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AcrCloudApiSdk.Models
{
    public class ReportResponseModel
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("result_type")]
        public long ResultType { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Metadata
    {
        public Metadata()
        {
            CustomFiles = new List<CustomFile>();
        }

        public int Id { get; set; }

        [JsonProperty("record_timestamp")]
        public string RecordTimestamp { get; set; }

        [JsonProperty("timestamp_utc")]
        public DateTime TimestampUtc { get; set; }

        [JsonProperty("played_duration")]
        public long PlayedDuration { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("program_title")]
        public string ProgramTitle { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelCountry { get; set; }
        public string AudioEvidence { get; set; }

        public DateTime? CreatedAt { get; set; }

        [JsonProperty("custom_files")]
        public ICollection<CustomFile> CustomFiles { get; set; }
    }

    public class CustomFile
    {
        [JsonProperty("play_offset_ms")]
        public long PlayOffsetMs { get; set; }

        [JsonProperty("sample_begin_time_offset_ms")]
        public long SampleBeginTimeOffsetMs { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sample_end_time_offset_ms")]
        public long SampleEndTimeOffsetMs { get; set; }

        [JsonProperty("duration_ms")]
        public string DurationMs { get; set; }

        [JsonProperty("bucket_id")]
        public string BucketId { get; set; }

        [JsonProperty("db_begin_time_offset_ms")]
        public long DbBeginTimeOffsetMs { get; set; }

        [JsonProperty("db_end_time_offset_ms")]
        public long DbEndTimeOffsetMs { get; set; }

        [JsonProperty("acrid")]
        public string Acrid { get; set; }

        [JsonProperty("audio_id")]
        public string AudioId { get; set; }
        public int MetadataId { get; set; }
        public Metadata Metadata { get; set; }
        public string ChannelType { get; set; }
        public string OrignalSongUrl { get; set; }

        public int GetPlayedDuration()
        {
            var hasConverted = int.TryParse(DurationMs, out int duration);
            if (hasConverted)
            {
                return duration / 1000;
            }
            return 0;
        }
    }

    public class Status
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }
}

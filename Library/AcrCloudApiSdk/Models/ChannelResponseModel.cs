using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AcrCloudApiSdk.Models
{
    public class ChannelResponseModel
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("_meta")]
        public Meta Meta { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("urls")]
        public List<object> Urls { get; set; }

        [JsonProperty("user_defined")]
        public List<object> UserDefined { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }
    }

    public partial class Self
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("pageCount")]
        public long PageCount { get; set; }

        [JsonProperty("currentPage")]
        public long CurrentPage { get; set; }

        [JsonProperty("perPage")]
        public long PerPage { get; set; }
    }
}

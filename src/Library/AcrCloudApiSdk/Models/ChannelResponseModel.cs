using System;
using System.Collections.Generic;

namespace AcrCloudApiSdk.Models
{
    public class ChannelResponseModel
    {
        public string errorStatus { get; set; }
        public string error { get; set; }
        public List<Item> items { get; set; }
        public Links _links { get; set; }
        public Meta _meta { get; set; }
    }

    public partial class Item
    {
        public long id { get; set; }
        public string state { get; set; }
        public string stream_name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string continent { get; set; }
        public Uri website { get; set; }
        public List<object> urls { get; set; }
        public List<object> user_defined { get; set; }
    }

    public partial class Links
    {
        public Self self { get; set; }
    }

    public partial class Self
    {
        public Uri href { get; set; }
    }

    public partial class Meta
    {
        public long totalCount { get; set; }
        public long pageCount { get; set; }
        public long currentPage { get; set; }
        public long perPage { get; set; }
    }
}

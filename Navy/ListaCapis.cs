using System;
using Newtonsoft.Json;

namespace Navy
{ 
    [JsonObject]
    public class ListaCapis
    {
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("urlLast")]
        public string UrlLast { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }    
}
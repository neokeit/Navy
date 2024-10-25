using System;
using Newtonsoft.Json;

namespace Navy
{
    [JsonObject]
    public class ResponseJk
    {
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
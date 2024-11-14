using System.Collections.Generic;
using Newtonsoft.Json;

namespace Navy.Models
{
    [JsonObject]
    public class ConfigModel
    {
        [JsonProperty("series")] 
        public List<SerieModel>? Series { get; set; }
    }
}

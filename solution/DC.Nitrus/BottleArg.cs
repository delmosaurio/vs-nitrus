using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleArg
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonIgnore]
        public bool ReadOnly { get; set; }
    }
}

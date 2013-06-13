using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Nitrus.Configuration;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class Workspace
    {
        #region Constructor(s)
        public Workspace()
        {
            
        }
        #endregion

        #region Members
        [JsonProperty(PropertyName = "modeltype")]
        public string ModelType { get; set; }

        [JsonProperty(PropertyName = "connString")]
        public string ConnString { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string ProviderName { get; set; }

        [JsonProperty("datacontext")]
        public Datacontext Datacontext { get; set; }

        [JsonIgnore]
        public BottleCollection Packages { get; set; }
        #endregion

    }
}

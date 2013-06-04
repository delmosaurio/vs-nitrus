using Newtonsoft.Json;

namespace DC.Nitrus.Configuration
{
    [JsonObject]
    public class WorkspaceConfigFile
    {

        #region Constructor
        public WorkspaceConfigFile(bool withDefault = false)
        {
            if (!withDefault) return;

            ModelType = "manual";
            ProviderName = "";
        }
        #endregion

        #region Members
        [JsonProperty(PropertyName = "modeltype")]
        public string ModelType { get; set; }

        [JsonProperty(PropertyName = "connString")]
        public string ConnString { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string ProviderName { get; set; }
        #endregion

    }
}

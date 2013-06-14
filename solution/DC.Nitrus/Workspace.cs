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
        #region Fields
        private BottleCollection _packages;

        private string _modelType = "manual";
        private string _connString = "";
        private string _providerName = "";
        #endregion

        #region Constructor(s)
        public Workspace(bool withDefaults = false)
        {
            if (!withDefaults) return;

            ModelType = "manual";
        }
        #endregion

        #region Members
        [JsonProperty(PropertyName = "modeltype")]
        public string ModelType
        {
            get { return _modelType;  }
            set { _modelType = value; }
        }

        [JsonProperty(PropertyName = "connString")]
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        [JsonProperty(PropertyName = "provider")]
        public string ProviderName
        {
            get { return _providerName; }
            set { _providerName = value; }
        }

        [JsonProperty(PropertyName = "datacontext")]
        public Datacontext Datacontext { get; set; }

        [JsonIgnore]
        public BottleCollection Packages
        {
            get { return _packages ?? (_packages = new BottleCollection()); }
        }
        #endregion

    }
}

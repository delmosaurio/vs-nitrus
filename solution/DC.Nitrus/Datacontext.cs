using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Model;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class Datacontext
    {
        #region Fields
        private BottleArgValueCollection _args;
        
        private LayerScopeCollection _scopes;
        #endregion

        #region Constructor
        public Datacontext(bool withDefaults = false)
        {
            if (!withDefaults) return;
        }
        #endregion
        
        #region Members
        [JsonProperty(PropertyName = "model")]
        public IModel Model { get; set; }

        #region Arguments
        [JsonIgnore]
        public BottleArgValueCollection Arguments
        {
            get { return _args ?? (_args = new BottleArgValueCollection());  }
            set { _args = value; }
        }

        // This solutions is for not create a custom JsonConverter
        [JsonProperty(PropertyName = "arguments")]
        public List<BottleArgValue> ListArguments
        {
            get
            {
                return this.Arguments.ToList();
            }
            set
            {
                _args = new BottleArgValueCollection();
                _args.AddRange(value);
            }
        }
        #endregion

        #region LayerScope
        [JsonIgnore]
        public LayerScopeCollection LayersScopes
        {
            get { return _scopes ?? (_scopes = new LayerScopeCollection()); }
            set { _scopes = value; }
        }

        // This solutions is for not create a custom JsonConverter
        [JsonProperty(PropertyName = "scope")]
        public List<LayerScope> ListLayersScopes
        {
            get
            {
                return LayersScopes.ToList();
            }
            set
            {
                _scopes = new LayerScopeCollection();
                _scopes.AddRange(value);
            }
        }
        #endregion

        #endregion

    }
}

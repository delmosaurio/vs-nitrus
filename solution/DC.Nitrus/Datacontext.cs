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

        [JsonProperty(PropertyName = "arguments")]
        public BottleArgValueCollection Arguments
        {
            get { return _args ?? (_args = new BottleArgValueCollection());  }
            set { _args = value; }
        }
        #endregion

    }
}

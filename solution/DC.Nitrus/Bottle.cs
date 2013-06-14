using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class Bottle
    {
        #region Fields
        private string _uid = "";

        private string _description = "";

        private BottleArgCollection _args;
        #endregion

        #region Constructor
        public Bottle(bool withDefaults = false)
        {
            if (!withDefaults) return;

        }
        #endregion

        #region Members
        [JsonProperty("uid")]
        public string Uid
        {
            get
            {
                return _uid;
            }
            set
            {
                _uid = value.ToLower();
            }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [JsonProperty("arguments")]
        public BottleArgCollection Arguments
        {
            get { return _args ?? (_args = new BottleArgCollection()); }
            set { _args = value; }
        }
        #endregion
    }
}

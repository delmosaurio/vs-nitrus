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

        private LayerCollection _layers;
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

        [JsonIgnore]
        public BottleArgCollection Arguments
        {
            get { return _args ?? (_args = new BottleArgCollection()); }
            set { _args = value; }
        }

        // This solutions is for not create a custom JsonConverter
        [JsonProperty("expects")]
        public List<BottleArg> ListArguments
        {
            get
            {
                return this.Arguments.ToList();
            }
            set
            {
                _args = new BottleArgCollection();
                _args.AddRange(value);
            }
        }
        
        [JsonIgnore]
        public LayerCollection Layes
        {
            get { return _layers ?? (_layers = new LayerCollection()); }
            set { _layers = value; }
        }

        #endregion
    }
}

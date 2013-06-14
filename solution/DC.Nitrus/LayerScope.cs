using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class LayerScope
    {
        #region Fields
        private Dictionary<string, bool> _scope;
        #endregion

        #region Constructor
        public LayerScope(string fullname)
        {
            Fullname = fullname;
        }
        #endregion

        #region Member

        public string Fullname { get; set; }

        [JsonProperty("scope")]
        public Dictionary<string, bool> Scope
        {
            get { return _scope ?? (_scope = new Dictionary<string, bool>()); }
            set { _scope = value; }
        }
        #endregion
        
    }
}

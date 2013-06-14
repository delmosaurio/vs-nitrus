using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class LayerCollection : List<Layer>
    {
        #region Fields
        #endregion

        #region Constructor
        #endregion

        #region Members
        public Layer this[string uid]
        {
            get { return this.SingleOrDefault(l => l.Uid.ToLower() == uid.ToLower()); }
        }
        #endregion
    }
}

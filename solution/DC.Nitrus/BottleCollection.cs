using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleCollection : List<Bottle>
    {
        #region Members
        public Bottle this[string uid]
        {
            get { return this.SingleOrDefault(b => b.Uid.ToLower() == uid.ToLower()); }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleArgCollection : List<BottleArg>
    {

        #region Members
        public BottleArg this[string name]
        {
            get { return this.SingleOrDefault(b => b.Name == name); }
        }
        #endregion

    }
}

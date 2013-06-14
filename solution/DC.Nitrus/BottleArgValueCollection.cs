using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleArgValueCollection : List<BottleArgValue>
    {
        #region Members
        public BottleArgValue this[string fullname]
        {
            get { return this.SingleOrDefault(a => a.Fullname.ToLower() == fullname.ToLower()); }
        }
        #endregion
    }
}

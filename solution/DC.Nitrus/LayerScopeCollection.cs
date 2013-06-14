using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Nitrus
{
    public class LayerScopeCollection : List<LayerScope>
    {
        #region Fields
        #endregion

        #region Constructor
        #endregion

        #region Members
        public LayerScope this[string fullname]
        {
            get { return this.SingleOrDefault(ls => ls.Fullname.ToLower() == fullname.ToLower()); }
        }

        public bool Contains(string fullname)
        {
            return
                this
                .Select(l => l.Fullname.ToLower())
                .Contains(fullname.ToLower());
        }
        #endregion

    }
}

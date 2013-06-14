using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Nitrus
{
    public sealed class ProjectsProvider
    {
        #region Fields

        #endregion

        #region Members

        public static IProjectsProvider CurrentProvider { get; set; }

        #endregion

    }
}

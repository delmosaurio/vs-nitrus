using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Nitrus;

namespace DC.Extensibility.Nitrus
{
    public class VsProjectsProvider : IProjectsProvider
    {
        public string[] ProjectNames { get; set; }
    }
}

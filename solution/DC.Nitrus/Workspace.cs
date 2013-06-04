using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Nitrus.Configuration;

namespace DC.Nitrus
{
    public class Workspace
    {
        public WorkspaceConfigFile Config { get; set; }

        public Datacontext Datacontext { get; set; }

        public PackageCollection Packages { get; set; }

    }
}

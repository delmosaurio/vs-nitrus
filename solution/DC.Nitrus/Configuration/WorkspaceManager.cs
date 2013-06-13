using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Nitrus.Configuration
{
    public class WorkspaceManager
    {

        public static Workspace Load(string path)
        {
            return new Workspace();
        }

        public static Workspace Create(string path = "")
        {
            return new Workspace();
        }

        public static bool Save(Workspace workspace)
        {
            
        }

    }
}

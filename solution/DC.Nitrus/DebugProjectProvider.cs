using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Nitrus
{
    public class DebugProjectProvider : IProjectsProvider
    {
        private string[] _projectNames = { "project1", "project2", "project3" };

        public string[] ProjectNames
        {
            get { return _projectNames; }
            set { _projectNames = value; }
        }
    }
}

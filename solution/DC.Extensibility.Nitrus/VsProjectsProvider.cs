using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DC.Nitrus;

namespace DC.Extensibility.Nitrus
{
    public class VsProjectsProvider : IProjectsProvider
    {

        private Regex _exclude = new Regex(@"\<miscfiles\>", RegexOptions.IgnoreCase);

        private string[] _projectNames { get; set; }

        public string[] ProjectNames
        {
            get { return _projectNames; }
            set 
            { 
                _projectNames = value.Where(
                        p => !_exclude.IsMatch(p)
                ).ToArray(); 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DC.Automation;
using DC.Nitrus.Configuration;

namespace DC.Nitrus.Explorer.Model
{
    public class PackagesTreeItem : TreeViewItem
    {

        #region Fields 
        private List<PackageConfig> _packages;
        #endregion

        #region Constructor
        public PackagesTreeItem(List<PackageConfig> packages)
        {
            _packages = packages;
            this.Tag = _packages;

            this.Header = "Packages";

            CreateNodes();
        }

        private void CreateNodes()
        {
            foreach(var pkg in _packages)
            {

                Items.Add(new PackageTreeItem(pkg));

            }
        }

        #endregion

        #region Helpers
        #endregion
    }
}

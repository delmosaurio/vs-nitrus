using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DC.Nitrus.Configuration;

namespace DC.Nitrus.Explorer.Model
{
    public class BottlesCollectionTreeItem : TreeViewItem
    {

        #region Fields 
        private BottleCollection _packages;
        #endregion

        #region Constructor
        public BottlesCollectionTreeItem(BottleCollection packages)
        {
            _packages = packages;
            this.Tag = _packages;

            this.Header = "Packages";

            CreateNodes();
        }

        private void CreateNodes()
        {
            foreach(var bottle in _packages)
            {
                Items.Add(new BottleTreeItem(bottle));

            }
        }

        #endregion

        #region Helpers
        #endregion
    }
}

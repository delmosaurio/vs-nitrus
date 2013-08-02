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
        private BottleCollection _bottles;
        #endregion

        #region Constructor
        public BottlesCollectionTreeItem(BottleCollection bottles)
        {
            _bottles = bottles;
            this.Tag = _bottles;

            this.Header = "Bottles";

            CreateNodes();
        }

        private void CreateNodes()
        {
            foreach(var bottle in _bottles)
            {
                Items.Add(new BottleTreeItem(bottle));

            }
        }

        #endregion

        #region Helpers
        #endregion
    }
}

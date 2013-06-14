using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DC.Nitrus.Configuration;

namespace DC.Nitrus.Explorer.Model
{
    public class BottleTreeItem : TreeViewItem
    {

        #region Fields
        private LayerCollection _layers;
        #endregion

        #region Constructor
        public BottleTreeItem(Bottle bottle)
        {
            this.Header = bottle.Uid;
            this.Tag = bottle;

            _layers = bottle.Layes;
            
            CreateNodes();

        }
        #endregion

        #region Helpers
        private void CreateNodes()
        {
         
            foreach (var l in _layers)
            {
                this.Items.Add(new LayerTreeItem(l));
            }
   
        }
        #endregion

    }
}

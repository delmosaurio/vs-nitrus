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
    public class BottleTreeItem : TreeViewItem
    {

        #region Fields
        private List<LayerConfig> _layers;
        #endregion

        #region Constructor
        public BottleTreeItem(PackageConfig pkg)
        {
            this.Header = pkg.PackageName;
            this.Tag = pkg;

            _layers = pkg.Layers;
            
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

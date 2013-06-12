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
    public class LayerTreeItem : TreeViewItem
    {
        public LayerTreeItem(LayerConfig packageLayer)
        {
            this.Header = packageLayer.LayerName;
            this.Tag = packageLayer;


        }
    }
}

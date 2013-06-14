using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DC.Nitrus.Configuration;

namespace DC.Nitrus.Explorer.Model
{
    public class LayerTreeItem : TreeViewItem
    {

        public LayerTreeItem(Layer packageLayer)
        {
            this.Header = packageLayer.Uid;
            this.Tag = packageLayer;
        }
    }
}

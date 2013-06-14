using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DC.Nitrus.Explorer.Model
{
    public class GeneralTreeItem : TreeViewItem 
    {
        public GeneralTreeItem(Workspace workspace)
        {
            this.Tag = workspace;
        }
    }
}

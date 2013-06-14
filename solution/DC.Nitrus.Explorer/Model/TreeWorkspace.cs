using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DC.Nitrus.Explorer.Model
{
    public class TreeWorkspace : List<TreeViewItem>
    {

        #region Fields
        private Workspace _workspace;
        #endregion

        #region Constructor
        public TreeWorkspace(Workspace workspace)
        {
            _workspace = workspace;
            
            CreateNodes();
        }
        #endregion

        #region Members
        public RootTreeItem Root { get; set; }

        public BottlesCollectionTreeItem Packages { get; set; }
        #endregion

        #region Helpers
        private void CreateNodes()
        {
            Root = new RootTreeItem(_workspace);
            Packages = new BottlesCollectionTreeItem(_workspace.Bottles);
            
            this.Add(Root);
            this.Add(Packages);

        }
        #endregion
    }
}

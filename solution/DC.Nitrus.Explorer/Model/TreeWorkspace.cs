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
        private NitrusWorkspace _workspace;
        #endregion

        #region Constructor
        public TreeWorkspace(NitrusWorkspace workspace)
        {
            _workspace = workspace;
            
            CreateNodes();
        }
        #endregion

        #region Members
        public RootTreeItem Root { get; set; }
        public PackagesTreeItem Packages { get; set; }
        #endregion

        #region Helpers
        private void CreateNodes()
        {
            Root = new RootTreeItem(_workspace);
            Packages = new PackagesTreeItem(_workspace.Config.Packages);
            
            this.Add(Root);
            this.Add(Packages);

        }
        #endregion
    }
}

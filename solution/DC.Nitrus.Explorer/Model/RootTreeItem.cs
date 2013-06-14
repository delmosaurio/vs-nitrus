using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DC.Nitrus.Explorer.Model
{
    public class RootTreeItem : TreeViewItem
    {

        #region Fields
        private Workspace _workspace;
        #endregion

        #region Constructor
        public RootTreeItem(Workspace workspace)
        {
            _workspace = workspace;
            this.Tag = _workspace;

            this.Header = "Nitrus";

            CreateNodes();
        }
        
        #endregion

        #region Helpers
        private void CreateNodes()
        {
            var general = new GeneralTreeItem(_workspace) { Header = "General" };
            this.Items.Add(general);

            var database = new DatabaseTreeItem(_workspace) { Header = "Model/Database" };
            this.Items.Add(database);

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DC.Nitrus.Configuration;
using DC.Nitrus.Explorer.Model;
using DC.Nitrus.Explorer.ViewControl;
using Control = System.Windows.Controls.Control;
using Label = System.Windows.Controls.Label;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace DC.Nitrus.Explorer
{
    /// <summary>
    /// Interaction logic for NitrusExplorer.xaml
    /// </summary>
    public partial class NitrusExplorer : Window
    {

        #region Fields
        private string _workspacePath = "";

        private Workspace _workspace;
        #endregion

        #region Constructor(s)
        public NitrusExplorer()
        {
            InitializeComponent();
            
            // initialize control instances
            
            this.GeneralControl = new NitrusGeneralControl();
            this.DatabaseControl = new DatabaseControl();
            this.BottlesCollectionControl = new BottlesCollectionControl();
            this.BottleControl = new BottleControl();
            this.LayerControl = new LayerControl();
            
            DataContext = this;
        }
        #endregion

        #region Members
        public Workspace GetCurrentWorkSpace()
        {
            return _workspace;
        }
        #endregion

        #region private members
        private void LoadWorkspace(string solutionPath = "", string[] projectNames = null)
        {
            _workspacePath = solutionPath;

            if (string.IsNullOrEmpty(_workspacePath))
            {
                var fd = new FolderBrowserDialog();

                if (fd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }

                _workspacePath = fd.SelectedPath;

            }

            var wk = WorkspaceManager.Load(_workspacePath);

            Load(wk);
        }

        public void LoadWorkspace(Workspace workspace, string path)
        {
            _workspacePath = path;
            Load(workspace);
        }

        private void Load(Workspace workspace)
        {
            _workspace = workspace;

            WorkspaceView = new TreeWorkspace(_workspace);
        }

        private NitrusGeneralControl GeneralControl { get; set; }

        private DatabaseControl DatabaseControl { get; set; }

        private BottlesCollectionControl BottlesCollectionControl { get; set; }

        private BottleControl BottleControl { get; set; }

        private LayerControl LayerControl { get; set; }

        #endregion

        #region View Members

        public Control CurrentView { get; set; }

        public TreeWorkspace WorkspaceView { get; set; }
        
        #endregion

        #region Handlers
        private void _mainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewItem)e.NewValue;

            if (node == null) { return; }
            
            var nodeType = e.NewValue.GetType().Name;
            
            switch (nodeType)
            {
                case ("RootTreeItem"):
                case ("GeneralTreeItem"):
                    CurrentView = this.GeneralControl;
                    break;
                case ("DatabaseTreeItem"):
                    CurrentView = this.DatabaseControl;
                    break;
                case ("BottlesTreeItem"):
                    CurrentView = this.BottlesCollectionControl;
                    break;
                case ("BottleTreeItem"):
                    CurrentView = this.BottleControl;
                    break;
                case ("LayerTreeItem"):
                    CurrentView = this.LayerControl;
                    break;
                default:
                    CurrentView = null;
                    break;
            }

            if (CurrentView != null)
            {
                CurrentView.DataContext = node.Tag;
            }
            
            _container.Content = CurrentView;
            
        }

        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_workspace == null) return;
            
            WorkspaceManager.Save( _workspace, _workspacePath, true );
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}

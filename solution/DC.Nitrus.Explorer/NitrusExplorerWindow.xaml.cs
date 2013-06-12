﻿using System;
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
using DC.Nitrus.Explorer.Model;
using DC.Nitrus.Explorer.ViewControl;
using Control = System.Windows.Controls.Control;
using Label = System.Windows.Controls.Label;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace DC.Nitrus.Explorer
{
    /// <summary>
    /// Interaction logic for NitrusExplorerWindow.xaml
    /// </summary>
    public partial class NitrusExplorerWindow : Window
    {

        #region Fields
        private string _solPath = "";

        private NitrusWorkspace _workspace;
        #endregion

        #region Constructor(s)
        public NitrusExplorerWindow()
        {
            InitializeComponent();
            
            // initialize control instances
            this.GeneralControl = new NitrusGeneralControl();
            this.DatabaseControl = new DatabaseControl();
            this.PackagesControl = new PackagesControl();
            this.PackageControl = new PackageControl();
            this.LayerControl = new LayerControl();
            
            // load the workspace
            //string[] pnames = { "Data", "Web"};

            //LoadWorkspace(@"D:\dev\data\ns", pnames);

            DataContext = this;
        }

        #endregion

        #region Members
        public NitrusWorkspace GetCurrentWorkSpace()
        {
            return _workspace;
        }
        #endregion

        #region private members
        private void LoadWorkspace(string solutionPath = "", string[] projectNames = null)
        {
            _solPath = solutionPath;

            if (string.IsNullOrEmpty(_solPath))
            {
                var fd = new FolderBrowserDialog();

                if (fd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }

                _solPath = fd.SelectedPath;

            }

            var wk = new NitrusWorkspace(_solPath, true, projectNames);

            LoadWorkspace(wk);
        }

        public void LoadWorkspace(NitrusWorkspace workspace)
        {
            _workspace = workspace;
            WorkspaceView = new TreeWorkspace(_workspace);
        }

        private NitrusGeneralControl GeneralControl { get; set; }

        private DatabaseControl DatabaseControl { get; set; }

        private PackagesControl PackagesControl { get; set; }

        private PackageControl PackageControl { get; set; }

        private LayerControl LayerControl { get; set; }

        #endregion

        #region View Members

        public Control CurrentView { get; set; }

        public TreeWorkspace WorkspaceView { get; set; }
        
        #endregion

        private void _mainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = (TreeViewItem)e.NewValue;

            if (node == null)
            {
                return;
            }
            
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
                case ("PackagesTreeItem"):
                    CurrentView = this.PackagesControl;
                    break;
                case ("PackageTreeItem"):
                    CurrentView = this.PackageControl;
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

            _workspace.SaveConfig();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
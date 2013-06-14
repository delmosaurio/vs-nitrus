using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using DC.Nitrus;
using DC.Nitrus.Configuration;
using DC.Nitrus.Explorer;
using EnvDTE;
using EnvDTE80;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Constants = EnvDTE.Constants;
using IServiceProvider = System.IServiceProvider;

namespace DC.Extensibility.Nitrus
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasSingleProject_string)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionHasMultipleProjects_string)]
    [Guid(GuidList.guidNitrusPkgString)]
    public sealed class NitrusPackage : Package, IVsSolutionEvents3
    {

        private DTE _dte;
        private IVsSolution solution = null;
        private uint _hSolutionEvents = uint.MaxValue;
        private DTEEvents _EventsObj;
        private uint _handleCookie;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public NitrusPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            this._dte = GetCurrentDTE();

            _EventsObj = _dte.Events.DTEEvents;
            _EventsObj.OnStartupComplete += OnStartupComplete;

            AdviseSolutionEvents();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (null == mcs) return;

            // 
            var menuCommandID = new CommandID(GuidList.guidNitrusCmdSet, (int)PkgCmdIDList.TopLevelMenu);
            var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
            mcs.AddCommand(menuItem);

            // [Nitrus->Compile All]
            var cmdidCompileAll = new CommandID(GuidList.guidNitrusCmdSet, (int)PkgCmdIDList.cmdidCompileAll);
            var menuCompileAll = new MenuCommand(CompileAll, cmdidCompileAll);
            mcs.AddCommand(menuCompileAll);

            // [Nitrus->Workspace ...]
            var cmdidWorkspaceOptions = new CommandID(GuidList.guidNitrusCmdSet, (int)PkgCmdIDList.cmdidWorkspaceOptions);
            var menuOpenWorskace = new MenuCommand(OpenWorkspace, cmdidWorkspaceOptions);
            mcs.AddCommand(menuOpenWorskace);

        }

        protected override void Dispose(bool disposing)
        {
            UnadviseSolutionEvents();

            base.Dispose(disposing);
        }

        private void AdviseSolutionEvents()
        {
            UnadviseSolutionEvents();

            solution = this.GetService(typeof(SVsSolution)) as IVsSolution;
            
            if (solution != null)
            {
                solution.AdviseSolutionEvents(this, out _handleCookie);
            }


        }

        private void UnadviseSolutionEvents()
        {
            if (solution != null)
            {
                if (_handleCookie != uint.MaxValue)
                {
                    solution.UnadviseSolutionEvents(_handleCookie);
                    _handleCookie = uint.MaxValue;
                }

                solution = null;
            }
        }
        #endregion
        
        #region Handlers
        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            // Do something
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            // Do something
            return VSConstants.S_OK;
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        { return VSConstants.S_OK; }

        public int OnAfterClosingChildren(IVsHierarchy pHierarchy)
        { return VSConstants.S_OK; }

        public int OnAfterMergeSolution(object pUnkReserved)
        { return VSConstants.S_OK; }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        { return VSConstants.S_OK; }

        public int OnAfterOpeningChildren(IVsHierarchy pHierarchy)
        { return VSConstants.S_OK; }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        { return VSConstants.S_OK; }

        public int OnBeforeClosingChildren(IVsHierarchy pHierarchy)
        { return VSConstants.S_OK; }

        public int OnBeforeOpeningChildren(IVsHierarchy pHierarchy)
        { return VSConstants.S_OK; }

        public int OnBeforeCloseSolution(object pUnkReserved)
        { return VSConstants.S_OK; }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        { return VSConstants.S_OK; }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        { return VSConstants.S_OK; }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        { return VSConstants.S_OK; }

        private void OnStartupComplete()
        {
            var sol = _dte.Solution;
        }
        #endregion

        #region Menu callbacks

        private void CompileAll(object sender, EventArgs e)
        {
            
            var uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));

            //var widows = (IVsUIHierarchyWindow)_dte.Windows.Item();
            
            IVsWindowFrame pFrame;
            object objVar;
            Guid guidSlnExplorer = new Guid(ToolWindowGuids.SolutionExplorer);
            uiShell.FindToolWindow(0, ref guidSlnExplorer, out pFrame);
            pFrame.GetProperty((int)__VSFPROPID.VSFPROPID_Hierarchy, out objVar);
            IVsHierarchy toolWindowContext = (IVsHierarchy)objVar;
            uint ctxCookie;
            //toolWindowContext.AddSubcontext(projItemContext, (int)VSUSERCONTEXTPRIORITY.VSUC_Priority_Selection, out ctxCookie);


            var clsid = GuidList.guidNitrusCmdSet;
            int result;
            
            uiShell.ShowMessageBox(
                0,
                ref clsid,
                "Nitrus",
                string.Format("Compile all !!"),
                string.Empty,
                0,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                OLEMSGICON.OLEMSGICON_INFO,
                0, // false
                out result);
        }

        private void OpenWorkspace(object sender, EventArgs e)
        {
            var uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));

            var clsid = GuidList.guidNitrusCmdSet;
            int result;

            if (_dte == null || solution == _dte.Solution) return;

            var ms = new SolutionManager(_dte.Solution);

            ProjectsProvider.CurrentProvider = new DebugProjectProcider();

            ProjectsProvider.CurrentProvider.ProjectNames = ms.ProjectNames.ToArray();

            var outPath = Path.Combine(ms.SolutionPath, "_Nitrus");
            
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }

            if (!WorkspaceManager.IsAWorkspace(outPath))
            {
                WorkspaceManager.Initialize(outPath);
            }

            var ws = WorkspaceManager.Initialize(outPath);

            var explorer = new NitrusExplorer();

            explorer.LoadWorkspace(ws);

            explorer.ShowDialog();

            /*
            uiShell.ShowMessageBox(
                0,
                ref clsid,
                "Nitrus",
                string.Format("Open Workspace !!"),
                string.Empty,
                0,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                OLEMSGICON.OLEMSGICON_INFO,
                0, // false
                out result);
            */

        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            // Show a Message Box to prove we were here
            var uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            var clsid = Guid.Empty;
            int result;
            ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
                       0,
                       ref clsid,
                       "Nitrus",
                       string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
                       string.Empty,
                       0,
                       OLEMSGBUTTON.OLEMSGBUTTON_OK,
                       OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
                       OLEMSGICON.OLEMSGICON_INFO,
                       0,        // false
                       out result)
            );
        }
        #endregion

        #region Helpers
        public static DTE GetCurrentDTE(IServiceProvider provider)
        {
            /*ENVDTE. */
            DTE vs = (DTE)provider.GetService(typeof(DTE));
            if (vs == null) throw new InvalidOperationException("DTE not found.");
            return vs;
        }

        public static DTE GetCurrentDTE()
        {
            return GetCurrentDTE(/* Microsoft.VisualStudio.Shell. */ServiceProvider.GlobalProvider);
        }
        #endregion

    }
}

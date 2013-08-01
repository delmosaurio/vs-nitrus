using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Nitrus;
using DC.Nitrus.Configuration;
using DC.Nitrus.Explorer;
using EnvDTE;

namespace DC.Extensibility.Nitrus
{
    public class Mediator
    {

        private Solution _solution;

        public Mediator(Solution solution)
        {
            if (solution == null)
            {
                throw new NoNullAllowedException("No solutions to works");
            }

            _solution = solution;
        }

        public void LaunchWorkspaceExplorer()
        {

            var ms = new SolutionManager(_solution);

            ProjectsProvider.CurrentProvider = new VsProjectsProvider();
            ProjectsProvider.CurrentProvider.ProjectNames = ms.ProjectNames.ToArray();

            var outPath = Path.Combine(ms.SolutionPath, "_Nitrus");

            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }

            var ws = WorkspaceManager.LoadOrInitialize(outPath);
            
            /*
            if (!WorkspaceManager.IsAWorkspace(outPath))
            {
                WorkspaceManager.LoadOrInitialize(outPath);
            }

            var ws = WorkspaceManager.Initialize(outPath);
            */

            var explorer = new NitrusExplorer();

            explorer.LoadWorkspace(ws);

            explorer.ShowDialog();

        }

    }
}

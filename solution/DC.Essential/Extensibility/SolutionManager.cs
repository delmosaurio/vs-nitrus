using System;
using System.Collections.Generic;
using EnvDTE;

namespace DC.Extensibility
{
    public class SolutionManager
    {

        #region Fields
        private Dictionary<string, int> _projectsIndex;
        private Dictionary<int, string> _projectsNames;
        private Solution _solution;
        private bool _intitialized;
        #endregion

        #region Contrustor(s)

        public SolutionManager(Solution solution)
        {
            Initialized(solution);
        }


        public SolutionManager()
        { }

        #endregion

        #region Members
        public string SolutionName
        {
            get
            {
                var solName = _solution.FullName;
                solName = solName.Substring(solName.LastIndexOf(@"\") + 1);
                solName = solName.Substring(0, solName.LastIndexOf("."));

                return solName;
            }
        }

        public string SolutionPath
        {
            get
            {
                return _solution.FullName.Substring(0, _solution.FullName.LastIndexOf(@"\"));
            }
        }

        public void Initialized(Solution solution)
        {

            _solution = solution;
            _intitialized = true;

            Refresh();

        }

        public Project this[string project]
        {
            get
            {
                return GetProject(project);
            }
        }

        public Projects Projects
        {
            get { return _solution.Projects; }
        }


        public void Refresh()
        {

            if (!_intitialized)
                throw new Exception("The class SolutionManager is not initialized");

            ProjectsIndex.Clear();

            var count = 1;
            foreach (Project project in Projects)
            {
                if (string.IsNullOrEmpty(project.UniqueName)) continue;

                if (project.UniqueName.ToLower() == "") continue;

                var str = project.UniqueName;
                if (project.UniqueName.IndexOf("\\") != -1)
                {
                    str = project.UniqueName.Substring(0, project.UniqueName.IndexOf("\\"));
                }

                ProjectsIndex.Add(str, count++);
            }

        }

        public IEnumerable<string> ProjectNames
        {
            get { return ProjectsIndex.Keys; }
        }

        #endregion

        #region Helpers
        private Dictionary<string, int> ProjectsIndex
        {
            get
            {
                if (_projectsIndex == null)
                { _projectsIndex = new Dictionary<string, int>(); }
                return _projectsIndex;
            }
        }

        private int GetIndexProject(string uniqueName)
        {
            return ProjectsIndex[uniqueName];
        }

        private Project GetProject(string uniqueName)
        {
            try
            {
                return Projects.Item(GetIndexProject(uniqueName));
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}

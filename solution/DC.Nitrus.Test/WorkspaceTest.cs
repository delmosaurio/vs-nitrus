using System;
using System.IO;
using DC.Nitrus.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DC.Nitrus.Test
{
    [TestClass]
    public class WorkspaceTest
    {
        #region Fields
        private string _cases = @"D:\dev\data\ns";
        #endregion

        #region Basics
        [TestMethod]
        public void Create()
        {
            var target = WorkspaceManager.Create();

            Assert.IsInstanceOfType(target, typeof(Workspace), "Is a workspace");
        }

        [TestMethod]
        public void Initialize()
        {
            var path = Path.Combine(_cases, "_bcase_initialize");

            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            var target = WorkspaceManager.Initialize(path);

            Assert.IsInstanceOfType(target, typeof(Workspace), "Is a workspace");
            
            Assert.IsTrue(
               File.Exists(Path.Combine(path, WorkspaceManager.DefaultFilename)),
               "The file was initialized"
           );
        }
        
        [TestMethod]
        public void Load()
        {
            var path = Path.Combine(_cases, "_bcase_load");

            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            // initialize
            WorkspaceManager.Initialize(path);
            
            
            // now load
            var target = WorkspaceManager.Load(path);

            Assert.IsInstanceOfType(target, typeof(Workspace), "The workspace was loaded");
        }

        [TestMethod]
        public void Save()
        {
            var path = Path.Combine(_cases, "_bcase_save");

            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            var target = WorkspaceManager.Create();

            WorkspaceManager.Save(target, path);
            
            Assert.IsTrue(
                File.Exists(
                    Path.Combine(path, WorkspaceManager.DefaultFilename)
                ),
                "The file was created"
            );
        }

        [TestMethod]
        public void FailSave()
        {
            var path = Path.Combine(_cases, "_bcase_failSave");

            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            // initialize
            WorkspaceManager.Initialize(path);
            
            try
            {
                var target = WorkspaceManager.Create();
                WorkspaceManager.Save(target, path);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(
                    ex.Message == "The path already have a workspace",
                    "The path already have a workspace"
                );
            }

        }
        #endregion

        #region Solution1
        [TestMethod]
        public void Solution1()
        {
            // project provider
            ProjectsProvider.CurrentProvider = new DebugProjectProvider();

            var path = Path.Combine(_cases, "solution1");
            
            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            // create bottles

            // bottle1
            var pkg1 = BottleManager.Initialize("package1", path);

            var arg1 = new BottleArg() { Name = "arg1", DefaultValue = "value1"};
            var arg2 = new BottleArg() { Name = "arg2", DefaultValue = "value2" };
            
            pkg1.Arguments.Add(arg1);
            pkg1.Arguments.Add(arg2);

            BottleManager.Save(pkg1, path, true);

            var lp = Path.Combine(path, "bottles/package1");

            Directory.CreateDirectory(Path.Combine(lp, "layer1"));
            Directory.CreateDirectory(Path.Combine(lp, "layer2"));
            Directory.CreateDirectory(Path.Combine(lp, "layer3"));

            // bottle2

            var pkg2 = BottleManager.Initialize("package2", path);

            pkg2.Arguments.Add(arg1);
            pkg2.Arguments.Add(arg2);

            BottleManager.Save(pkg2, path, true);

            lp = Path.Combine(path, "bottles/package2");

            Directory.CreateDirectory(Path.Combine(lp, "layer1"));
            
            // bottle3
            var pkg3 = BottleManager.Initialize("package3", path);

            pkg3.Arguments.Add(arg1);

            BottleManager.Save(pkg3, path, true);

            lp = Path.Combine(path, "bottles/package3");

            Directory.CreateDirectory(Path.Combine(lp, "layer1"));
            Directory.CreateDirectory(Path.Combine(lp, "layer3"));

            var ws = WorkspaceManager.Initialize(path);

        }
        #endregion

    }
}

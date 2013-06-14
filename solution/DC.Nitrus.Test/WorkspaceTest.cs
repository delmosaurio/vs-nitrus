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
            var path = Path.Combine(_cases, "solution1");

            // remove all first
            if (Directory.Exists(path)) Directory.Delete(path, true);

            // create bottles
            BottleManager.Initialize("package1", path);
            BottleManager.Initialize("package2", path);
            BottleManager.Initialize("package3", path);
            
        }
        #endregion

    }
}

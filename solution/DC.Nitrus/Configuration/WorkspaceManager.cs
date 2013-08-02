using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DC.Utils;

namespace DC.Nitrus.Configuration
{
    public class WorkspaceManager
    {

        #region Members
        public static string DefaultFilename = "workspace.json";

        public static string DefaultFolder = "_Nitrus";

        public static bool IsAWorkspace(string path)
        {
            var e = Directory.Exists(path);

            if (!e)
                throw new Exception("The path not exists");

            var rex = new Regex(DefaultFilename, RegexOptions.IgnoreCase);

            return Directory.GetFiles(path).Any(rex.IsMatch);
        }
        
        public static Workspace LoadOrInitialize(string path)
        {
            if (Directory.Exists(path) && IsAWorkspace(path))
            {
                return Load(path);
            }

            return Initialize(path);
        }

        public static Workspace Load(string path)
        {
            var ws = ConfigReader.Read<Workspace>(Path.Combine(path, DefaultFilename));

            LoadContext(ws, path);

            Debug.WriteLine("The workspace was loaded");

            return ws;
        }

        protected static void LoadContext(Workspace ws, string path)
        {
            var bpath = Path.Combine(path, BottleManager.RelativePath);

            if (!Directory.Exists(bpath))
            {
                Debug.WriteLine("The botles path not exists.");
                return;
            }

            // read bottles in the ´path´
            var pdir = new DirectoryInfo(bpath);
            
            var folders = pdir.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (!folders.Any())
            {
                Debug.WriteLine("No bottles to load.");
                return;
            }

            var bottles = folders
                              .Select(f => f.FullName)
                              .Where(BottleManager.IsABottle)
                              .Select(BottleManager.Load);

            ws.Bottles.AddRange(bottles);   
            
            // refresh de context
            var ctx = ws.Context;
            var expects = ws.Bottles.SelectMany(b => b.Arguments);

            // add arg if not exist
            // with default value
            var argsToAdd = expects
                        .Where(arg => !ctx.Arguments.Contains(arg.Fullname))
                        .Select(arg => new BottleArgValue(arg.Fullname, arg.DefaultValue));

            ctx.Arguments.AddRange(argsToAdd);

            // fill the bottle args
            var args = ws.Bottles.SelectMany(b => b.Arguments);

            foreach (var bottleArg in args)
            {
                var wba = ctx.Arguments[bottleArg.Fullname];
                bottleArg.Value = wba.Value;
            }

            // add layers if not exist
            var layers = ws.Bottles.SelectMany(b => b.Layes);
            
            var lyToAdd = layers
                        .Where(l => !ctx.LayersScopes.Contains(l.Fullname))
                        .Select(l => new LayerScope(l.Fullname));
            
            ctx.LayersScopes.AddRange(lyToAdd);

            // fill layer scope
            foreach (var ls in ctx.LayersScopes)
            {
                foreach (var pn in ProjectsProvider.CurrentProvider.ProjectNames.Where(pn => !ls.Scope.ContainsKey(pn)))
                {
                    ls.Scope.Add(pn, false);
                }
            }

            foreach (var ls in layers)
            {
                var cls = ctx.LayersScopes[ls.Fullname];
                ls.LayerScope.AddRange(cls.Scope.Select(s => new LayerScopeBindable() { Name = s.Key, Selected = s.Value }));
            }
        }

        public static Workspace Create()
        {
            return new Workspace(true);
        }

        public static Workspace Initialize(string path = "", bool force = false)
        {
            var ws = Create();
            
            if (string.IsNullOrEmpty(path)) return ws;

            LoadContext(ws, path);
            
            Save(ws, path, force);

            Debug.WriteLine("The workspace was initialized");

            return ws;
        }

        public static void Save(Workspace workspace, string path, bool force = false)
        {
            var dir= new DirectoryInfo(path); ;

            if (!Directory.Exists(path))
            {
                dir = Directory.CreateDirectory(path);
            }
            else if (Directory.Exists(path) && !force)
            {
                var rex = new Regex(DefaultFilename, RegexOptions.IgnoreCase);

                if (Directory.GetFiles(path).Any(rex.IsMatch) && IsAWorkspace(path))
                {
                    throw new Exception("The path already have a workspace");
                }
            }

            var filename = Path.Combine(dir.FullName, DefaultFilename);

            // fill the workspace args from Bottles objects
            var args = workspace.Bottles.SelectMany(b => b.Arguments);

            foreach (var bottleArg in args)
            {
                var wba = workspace.Context.Arguments[bottleArg.Fullname];
                wba.Value = bottleArg.Value;
            }


            // fill layer scope
            var layers = workspace.Bottles.SelectMany(b => b.Layes);

            foreach (var ls in layers)
            {
                var cls = workspace.Context.LayersScopes[ls.Fullname];

                foreach (var bindedlScope in ls.LayerScope)
                {
                    cls.Scope[bindedlScope.Name] = bindedlScope.Selected;
                }
                
            }

            // write file
            ConfigWriter.Write(workspace, filename);

            // create others folders

            // ./bottles
            Directory.CreateDirectory(Path.Combine(dir.FullName, BottleManager.RelativePath));

            Debug.WriteLine("The workspace was saved");

        }
        #endregion

    }
}

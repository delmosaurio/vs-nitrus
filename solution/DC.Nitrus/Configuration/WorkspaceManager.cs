using System;
using System.Collections.Generic;
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

        public static bool IsAWorkspace(string path)
        {
            var e = Directory.Exists(path);

            if (!e)
                throw new Exception("The path is empty");

            var rex = new Regex(DefaultFilename, RegexOptions.IgnoreCase);

            return Directory.GetFiles(path).Any(rex.IsMatch);
        }

        public static Workspace Load(string path)
        {
            var ws = ConfigReader.Read<Workspace>(Path.Combine(path, DefaultFilename));

            LoadContext(ws, path);

            return ws;
        }

        protected static void LoadContext(Workspace ws, string path)
        {
            var bpath = Path.Combine(path, BottleManager.RelativePath);

            // read bottles in the ´path´
            var pdir = new DirectoryInfo(bpath);

            var folders = pdir.GetDirectories("*", SearchOption.TopDirectoryOnly);

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

            // add layers if not exist
            var layers = ws.Bottles.SelectMany(b => b.Layes);
            
            var lyToAdd = layers
                        .Where(l => !ctx.LayersScopes.Contains(l.Fullname))
                        .Select(l => new LayerScope(l.Fullname));
            
            ctx.LayersScopes.AddRange(lyToAdd);
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

            return ws;
        }

        public static void Save(Workspace workspace, string path, bool force = false)
        {
            DirectoryInfo dir;

            if (!Directory.Exists(path))
            {
                dir = Directory.CreateDirectory(path);
            }
            else if (Directory.Exists(path) && !force)
            {
                var rex = new Regex(DefaultFilename, RegexOptions.IgnoreCase);

                if (Directory.GetFiles(path).Any(rex.IsMatch))
                {
                    throw new Exception("The path already have a workspace");
                }

                dir = new DirectoryInfo(path);
            }

            var filename = Path.Combine(path, DefaultFilename);

            ConfigWriter.Write(workspace, filename);
        }
        #endregion

    }
}

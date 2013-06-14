﻿using System;
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

        public static Workspace Load(string path)
        {
            var ws = ConfigReader.Read<Workspace>(Path.Combine(path, DefaultFilename));

            return ws;
        }

        public static Workspace Create()
        {
            return new Workspace(true);
        }

        public static Workspace Initialize(string path = "", bool force = false)
        {
            var ws = Create();
            
            if (string.IsNullOrEmpty(path)) return ws;

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

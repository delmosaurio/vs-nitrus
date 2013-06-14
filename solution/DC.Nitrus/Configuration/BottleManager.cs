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
    public class BottleManager
    {
        
        #region Members
        public static string RelativePath = "bottles";

        public static string DefaultFilename = "bottle.json";

        public static bool IsABottle(string path)
        {
            var e = Directory.Exists(path);

            if (!e) 
                throw new Exception("The path is empty");

            var rex = new Regex(DefaultFilename, RegexOptions.IgnoreCase);

            return Directory.GetFiles(path).Any(rex.IsMatch);
        }

        public static Bottle Load(string path)
        {
            if ( !IsABottle(path) ) 
                throw new Exception("The path not contains a bottle");

            // read file
            var bottle = ConfigReader.Read<Bottle>(Path.Combine(path, DefaultFilename));

            // load arguments
            foreach (var arg in bottle.Arguments)
            {
                arg.Owner = bottle;
            }

            // load layers
            var pdir = new DirectoryInfo(path);

            var folders = pdir.GetDirectories("*", SearchOption.TopDirectoryOnly);

            var layers = folders
                          .Select(f => new Layer(f.FullName, bottle));

            bottle.Layes.AddRange(layers);

            return bottle;
        }

        public static Bottle Create()
        {
            return new Bottle(true);
        }

        public static Bottle Initialize(string uid, string path = "", bool force = false)
        {
            var b = Create();

            b.Uid = uid;

            if (string.IsNullOrEmpty(path)) return b;

            Save(b, path, force);

            return b;
        }

        public static void Save(Bottle bottle, string workspacePath, bool force = false)
        {

            var path = Path.Combine(workspacePath, "bottles", bottle.Uid);

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
                    throw new Exception("The path already have a bottle");
                }

                dir = new DirectoryInfo(path);
            }

            var filename = Path.Combine(path, DefaultFilename);

            ConfigWriter.Write(bottle, filename);
        }
        #endregion

    }
}

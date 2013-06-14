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
        public static string DefaultFilename = "package.json";

        public static Bottle Load(string path)
        {
            var bottle = ConfigReader.Read<Bottle>(Path.Combine(path, DefaultFilename));

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

            var output = Path.Combine(path, uid);

            Save(b, output, force);

            return b;
        }

        public static void Save(Bottle bottle, string path, bool force = false)
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

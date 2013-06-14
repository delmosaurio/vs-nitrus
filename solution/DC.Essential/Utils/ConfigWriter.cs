using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DC.Utils
{
    public class ConfigWriter
    {

        #region Members
        public static void Write<T>(T config, string filename) where T : class
        {
            var extension = Path.GetExtension(filename);
            Write(config, filename, extension);
        }

        public static void Write<T>(T config, string filename, string extension) where T : class
        {
            if (extension == null) return;


            var ext = extension.ToLower();

            switch (ext)
            {
                case "json":
                case ".json":
                    WriteJson<T>(config, filename);
                    break;
                case "xml":
                case ".xml":
                    WriteXml<T>(config, filename);
                    break;
            }
        }

        public static void WriteXml<T>(T config, string filename)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = File.OpenWrite(filename))
            {
                serializer.Serialize(stream, config);
                stream.Close();
            }
            
        }

        public static void WriteJson<T>(T config, string filename, Formatting formatting = Formatting.Indented)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(config, formatting));
        }
        #endregion

    }
}

using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DC.Utils
{
    public class ConfigReader
    {

        static ConfigReader()
        { }
        
        public static T Read<T>(string filename) where T : class
        {
            var extension = Path.GetExtension(filename);
            return Read<T>(filename, extension);
        }

        public static T Read<T>(string filename, string extension) where T : class
        {
            if (extension == null) return null;
            
            var ext = extension.ToLower();

            switch(ext)
            {
                case "json":
                case ".json":
                    return ReadJson<T>(filename);
                case "xml":
                case ".xml":
                    return ReadXml<T>(filename);
                default:
                    return null;
            }
            
        }

        public static T ReadXml<T>(string filename)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(File.OpenRead(filename));
        }

        public static T ReadJson<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<T>(json);
        }
        
    }
}

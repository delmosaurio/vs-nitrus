using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class Layer
    {
        
        #region Fields
        private string _uid = "";
        #endregion

        #region Constructor
        public Layer(string path, Bottle owner)
        {
            Owner = owner;

            if (!Directory.Exists(path)) return;

            var df = new DirectoryInfo(path);
            this.Uid = df.Name;
        }
        #endregion

        #region Member
        [JsonProperty("uid")]
        public string Uid
        {
            get { return _uid; }
            set { _uid = value.ToLower(); }
        }

        [JsonIgnore]
        public Bottle Owner { get; set; }

        [JsonIgnore]
        public string Fullname
        {
            get
            {
                if (Owner == null)
                {
                    throw new Exception("The owner can not be null");
                }

                return string.Format("{0}.{1}", Owner.Uid, this.Uid);
            }
        }
        #endregion

    }
}

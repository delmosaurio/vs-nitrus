﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleArg
    {
        #region Members
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "dafultValue")]
        public string DefaultValue { get; set; }

        [JsonProperty(PropertyName = "desciption")]
        public string Description { get; set; }

        [JsonIgnore]
        public bool ReadOnly { get; set; }

        [JsonIgnore]
        public Bottle Owner { get; set; }

        public string Fullname
        {
            get
            {
                if (Owner == null)
                {
                    throw new Exception("The owner can not be null");
                }

                return string.Format("{0}.{1}", Owner.Uid, this.Name);
            }
        }
        #endregion
    }
}

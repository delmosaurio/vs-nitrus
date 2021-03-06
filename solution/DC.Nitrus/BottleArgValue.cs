﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class BottleArgValue
    {
        #region Constrcutor
        public BottleArgValue(string fullname, string value)
        {
            Fullname = fullname;
            Value = value;
        }
        #endregion

        #region Members
        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("value")]
        public string Value{ get; set; }
        #endregion
    }
}

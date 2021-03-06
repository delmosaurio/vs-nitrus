﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Nitrus.Configuration;
using Newtonsoft.Json;

namespace DC.Nitrus
{
    [JsonObject]
    public class Workspace
    {
        #region Fields
        private BottleCollection _bottles;

        private Datacontext _context;

        private string _modelType = "manual";
        private string _connString = "";
        private string _providerName = "";
        #endregion

        #region Constructor(s)
        public Workspace(bool withDefaults = false)
        {
            if (!withDefaults) return;

            ModelType = "manual";
        }
        #endregion

        #region Members
        [JsonProperty(PropertyName = "modeltype")]
        public string ModelType
        {
            get { return _modelType;  }
            set { _modelType = value; }
        }

        [JsonProperty(PropertyName = "connString")]
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        [JsonProperty(PropertyName = "provider")]
        public string ProviderName
        {
            get { return _providerName; }
            set { _providerName = value; }
        }

        [JsonProperty(PropertyName = "context")]
        public Datacontext Context
        {
            get { return _context ?? (_context = new Datacontext(true)); }
            set { _context = value; }
        }

        [JsonIgnore]
        public IProjectsProvider ProjectProvider
        {
            get { return ProjectsProvider.CurrentProvider; }
        }

        [JsonIgnore]
        public BottleCollection Bottles
        {
            get { return _bottles ?? (_bottles = new BottleCollection()); }
        }
        #endregion

    }
}

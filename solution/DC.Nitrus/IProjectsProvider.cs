﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Nitrus
{
    public interface IProjectsProvider
    {
        string[] ProjectNames { get; set; }
    }
}

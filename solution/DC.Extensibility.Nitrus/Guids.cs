// Guids.cs
// MUST match guids.h
using System;

namespace DC.Extensibility.Nitrus
{
    static class GuidList
    {
        public const string guidNitrusPkgString = "f4e0acc2-6e1f-474a-a9d0-bec548a0bfa3";
        public const string guidNitrusCmdSetString = "e31c43af-f9cb-4d74-90d6-68545a491819";

        public static readonly Guid guidNitrusCmdSet = new Guid(guidNitrusCmdSetString);
    };
}
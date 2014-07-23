// Guids.cs
// MUST match guids.h
using System;

namespace VSServiceStack
{
    static class GuidList
    {
        public const string guidVSServiceStackPkgString = "97413fa1-bad9-4cfb-a91c-c8d7b2c3c844";
        public const string guidVSServiceStackCmdSetString = "a5be67cb-5ac4-4a73-bc8b-7b8dc4556aee";

        public static readonly Guid guidVSServiceStackCmdSet = new Guid(guidVSServiceStackCmdSetString);
    };
}
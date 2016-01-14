// Guids.cs
// MUST match guids.h
using System;

namespace ServiceStackVS
{
    static class GuidList
    {
        public const string guidVSServiceStackPkgString = "97413fa1-bad9-4cfb-a91c-c8d7b2c3c844";
        public const string guidVSServiceStackCmdSetString = "a5be67cb-5ac4-4a73-bc8b-7b8dc4556aee";
        public const string guidServiceStackVSOutputWindowPane = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";
        public const string guidOptionsDialog = "9472fd90-2b49-42ac-bdb4-505afe0a7717";

        public static readonly Guid guidVSServiceStackCmdSet = new Guid(guidVSServiceStackCmdSetString);
    };
}
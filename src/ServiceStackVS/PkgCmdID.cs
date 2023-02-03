// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace ServiceStackVS
{
    static class PkgCmdIDList
    {
        public const uint cmdidCSharpAddServiceStackReference =     0x0100;
        public const uint cmdidUpdateServiceStackReference = 0x0104;
        public const uint cmdidFSharpAddServiceStackReference =     0x0101;
        //public const uint cmdidFSharpUpdateServiceStackReference =  0x0201;
        public const uint cmdidVbNetAddServiceStackReference = 0x0102;
        //public const uint cmdidVbNetUpdateServiceStackReference = 0x0202;
        public const uint cmdidTypeScriptAddServiceStackReference = 0x0103;
        //public const uint cmdidTypeScriptUpdateServiceStackReference = 0x0203;

    };
}
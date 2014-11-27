using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using ServiceStack;
using ServiceStackVS.FileHandlers;

namespace ServiceStackVS
{
    public static class NativeTypeHandlers
    {
        public static readonly CSharpNativeTypesHandler CSharpNativeTypesHandler = new CSharpNativeTypesHandler();
        public static readonly FSharpNativeTypesHandler FSharpNativeTypesHandler = new FSharpNativeTypesHandler();
        public static readonly VbNetNativeTypesHandler VbNetNativeTypesHandler = new VbNetNativeTypesHandler();
        public static readonly TypeScriptNativeTypesHandler TypeScriptNativeTypesHandler = new TypeScriptNativeTypesHandler();

        public static readonly List<INativeTypesHandler> All = new List<INativeTypesHandler>
        {
            CSharpNativeTypesHandler,
            FSharpNativeTypesHandler,
            VbNetNativeTypesHandler,
            TypeScriptNativeTypesHandler
        }; 
    }
}

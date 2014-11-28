using System.Collections.Generic;
using ServiceStackVS.NativeTypes.Handlers;

namespace ServiceStackVS.NativeTypes
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

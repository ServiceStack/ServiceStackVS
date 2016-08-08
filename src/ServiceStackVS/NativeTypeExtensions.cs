using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using NuGet.VisualStudio;
using ServiceStackVS.NativeTypes;

namespace ServiceStackVS
{
    public static class NativeTypeExtensions
    {
        public static List<string> ValidVsProjectTypeGuids(this INativeTypesHandler handler)
        {
            if (handler.TypesLanguage == NativeTypesLanguage.CSharp)
            {
                return new List<string> { VsHelperGuids.CSharpProjectKind };
            }

            if (handler.TypesLanguage == NativeTypesLanguage.FSharp)
            {
                return new List<string> { VsHelperGuids.FSharpProjectKind };
            }

            if (handler.TypesLanguage == NativeTypesLanguage.VbNet)
            {
                return new List<string> { VsHelperGuids.VbNetProjectKind };
            }

            if (handler.TypesLanguage == NativeTypesLanguage.TypeScript ||
                handler.TypesLanguage == NativeTypesLanguage.TypeScriptConcrete)
            {
                return new List<string> 
                { 
                    VsHelperGuids.CSharpProjectKind,
                    VsHelperGuids.FSharpProjectKind,
                    VsHelperGuids.VbNetProjectKind 
                };
            }
            return new List<string>();
        }
    }

    public static class VsHelperGuids
    {
        public const string CSharpProjectKind = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
        public const string FSharpProjectKind = "{F2A71F9B-5D33-465A-A702-920D77279786}";
        public const string VbNetProjectKind = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";

        public const string CSharpDotNetCore = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";

        public const string ProjectUnloaded = "{67294A52-A4F0-11D2-AA88-00C04F688DDE}";

    }

    public static class EnvDteConstants
    {
        public const string vsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";
    }
}

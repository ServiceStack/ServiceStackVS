using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class EsModuleNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.Mjs;
        public override string CodeFileExtension => ".dtos.mjs";
        public override string RelativeTypesUrl => "types/mjs";

        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EqualsIgnoreCase("dtos.ts");
        }
    }
}

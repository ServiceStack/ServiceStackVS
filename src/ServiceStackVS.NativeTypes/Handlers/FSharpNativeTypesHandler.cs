using System.Collections.Generic;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class FSharpNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.FSharp; } }
        public override string CodeFileExtension { get { return ".dtos.fs"; } }
        public override string RelativeTypesUrl { get { return "types/fsharp"; } }

        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EndsWith(".dto.fs");
        }

        private readonly List<string> requiredNuGetPackages = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };
        public override List<string> RequiredNuGetPackages
        {
            get { return requiredNuGetPackages; }
        }
    }
}

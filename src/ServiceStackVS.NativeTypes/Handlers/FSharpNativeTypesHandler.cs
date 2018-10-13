using System.Collections.Generic;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class FSharpNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.FSharp;
        public override string CodeFileExtension => ".dtos.fs";
        public override string RelativeTypesUrl => "types/fsharp";

        public override bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension) || fileName.EndsWith(".dto.fs");
        }

        public override List<string> RequiredNuGetPackages { get; } = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };
    }
}

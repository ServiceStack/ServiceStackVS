using System.Collections.Generic;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class CSharpNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.CSharp; } }
        public override string CodeFileExtension { get { return ".dtos.cs"; } }
        public override string RelativeTypesUrl { get { return "types/csharp"; } }

        private readonly List<string> requiredNuGetPackages = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };
        public override List<string> RequiredNuGetPackages
        {
            get { return requiredNuGetPackages; }
        }
    }
}

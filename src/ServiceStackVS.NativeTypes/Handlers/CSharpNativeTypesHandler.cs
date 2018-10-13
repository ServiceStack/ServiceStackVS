using System.Collections.Generic;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class CSharpNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.CSharp;
        public override string CodeFileExtension => ".dtos.cs";
        public override string RelativeTypesUrl => "types/csharp";

        public override List<string> RequiredNuGetPackages { get; } = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };
    }
}

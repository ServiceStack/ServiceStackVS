using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class VbNetNativeTypesHandler : BaseNativeTypesHandler, INativeTypesHandler
    {
        public override Dictionary<string, string> ParseComments(string codeOutput)
        {
            var commentLines = codeOutput.ExtractFirstCommentBlock(TypesLanguage);

            var overriddenProperties =
                commentLines.Where(x =>
                    x.TrimStart().StartsWith("'") &&
                    x.TrimStart().StartsWith("'''") == false)
                    .Select(x =>
                    {
                        x = x.TrimStart('\'');
                        return x;
                    })
                    .Join(Environment.NewLine)
                    .ParseKeyValueText(":");
            return overriddenProperties;
        }

        public override NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.VbNet; } }
        public override string CodeFileExtension { get { return ".dtos.vb"; } }
        public override string RelativeTypesUrl { get { return "types/vbnet"; } }

        private readonly List<string> requiredNuGetPackages = new List<string> { "ServiceStack.Text", "ServiceStack.Client" }; 
        public override List<string> RequiredNuGetPackages
        {
            get { return requiredNuGetPackages; }
        }
    }
}

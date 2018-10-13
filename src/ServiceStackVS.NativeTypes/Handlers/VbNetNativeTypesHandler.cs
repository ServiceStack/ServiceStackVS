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

        public override NativeTypesLanguage TypesLanguage => NativeTypesLanguage.VbNet;
        public override string CodeFileExtension => ".dtos.vb";
        public override string RelativeTypesUrl => "types/vbnet";

        public override List<string> RequiredNuGetPackages { get; } = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class VbNetNativeTypesHandler : INativeTypesHandler
    {
        public Dictionary<string, string> ParseComments(string codeOutput)
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

        public string GenerateUrl(string baseUrl, Dictionary<string, string> options)
        {
            return (baseUrl.WithTrailingSlash() + RelativeTypesUrl).BuildTypesUrlWithQueryStringValues(options);
        }

        public string GetUpdatedCode(string baseUrl, Dictionary<string, string> options)
        {
            string url = GenerateUrl(baseUrl, options);
            var webRequest = WebRequest.Create(url);
            string result = webRequest.GetResponse().ReadToEnd();
            return result;
        }

        public NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.VbNet; } }

        public string CodeFileExtension { get { return ".dtos.vb"; } }
        public string RelativeTypesUrl { get { return "types/vbnet"; } }

        public bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension);
        }

        private readonly List<string> validProjectGuids = new List<string>
                {
                    VsHelperGuids.VbNetProjectKind
                };

        public List<string> ValidVsProjectTypeGuids
        {
            get { return validProjectGuids; }
        }
    }
}

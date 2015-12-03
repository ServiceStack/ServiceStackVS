using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public class CSharpNativeTypesHandler : INativeTypesHandler
    {
        public Dictionary<string, string> ParseComments(string codeOutput)
        {
            var commentLines = codeOutput.ExtractFirstCommentBlock(TypesLanguage);

            var overriddenProperties =
                commentLines.Where(x =>
                    Regex.IsMatch(x.TrimStart(), "^[a-zA-Z]"))
                    .ToList()
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
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            string result = webRequest.GetResponse().ReadToEnd();
            return result;
        }

        public NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.CSharp; } }

        public string CodeFileExtension { get { return ".dtos.cs"; } }
        public string RelativeTypesUrl { get { return "types/csharp"; } }
        public bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension);
        }

        private readonly List<string> requiredNuGetPackages = new List<string> { "ServiceStack.Text", "ServiceStack.Client" };

        public List<string> RequiredNuGetPackages
        {
            get { return requiredNuGetPackages; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using ServiceStack;

namespace ServiceStackVS.NativeTypes.Handlers
{
    public abstract class BaseNativeTypesHandler
    {
        public virtual Dictionary<string, string> ParseComments(string codeOutput)
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

        public virtual string GenerateUrl(string baseUrl, Dictionary<string, string> options)
        {
            return (baseUrl.WithTrailingSlash() + RelativeTypesUrl).BuildTypesUrlWithQueryStringValues(options);
        }

        public abstract string RelativeTypesUrl { get; }

        public abstract string CodeFileExtension { get;  }

        public abstract NativeTypesLanguage TypesLanguage { get; }

        public virtual string GetUpdatedCode(string baseUrl, Dictionary<string, string> options)
        {
            string url = GenerateUrl(baseUrl, options);
            var webRequest = WebRequest.Create(url);
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            string result = webRequest.GetResponse().ReadToEnd();
            return result;
        }

        public virtual bool IsHandledFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }
            return fileName.EndsWithIgnoreCase(CodeFileExtension);
        }

        public virtual List<string> RequiredNuGetPackages
        {
            get { return new List<string>(); }
        }
    }
}

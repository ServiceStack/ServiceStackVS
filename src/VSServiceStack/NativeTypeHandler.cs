using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using EnvDTE;
using ServiceStack;

namespace ServiceStackVS
{
    public interface INativeTypesHandler
    {
        Dictionary<string, string> ParseComments(string codeOutput);
        string GenerateUrl(string baseUrl, Dictionary<string, string> options);
        string GetUpdatedCode(string baseUrl, Dictionary<string, string> options);
        NativeTypesLanguage TypesLanguage { get; }
        string CodeFileExtension { get; }
    }

    public enum NativeTypesLanguage
    {
        CSharp,
        FSharp,
        VbNet
    }

    public class CSharpNativeTypesHandler : INativeTypesHandler
    {
        public Dictionary<string,string> ParseComments(string codeOutput)
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
            return (baseUrl.WithTrailingSlash() + "types/" + TypesLanguage).BuildTypesUrlWithQueryStringValues(options);
        }

        public string GetUpdatedCode(string baseUrl, Dictionary<string, string> options)
        {
            string url = GenerateUrl(baseUrl, options);
            var webRequest = WebRequest.Create(url);
            string result = webRequest.GetResponse().ReadToEnd();
            return result;
        }

        public NativeTypesLanguage TypesLanguage { get {  return NativeTypesLanguage.CSharp; } }

        public string CodeFileExtension { get { return ".dtos.cs"; } }
    }

    public class FSharpNativeTypesHandler : INativeTypesHandler
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
            return (baseUrl.WithTrailingSlash() + "types/" + TypesLanguage).BuildTypesUrlWithQueryStringValues(options);
        }

        public string GetUpdatedCode(string baseUrl, Dictionary<string, string> options)
        {
            string url = GenerateUrl(baseUrl, options);
            var webRequest = WebRequest.Create(url);
            string result = webRequest.GetResponse().ReadToEnd();
            return result;
        }

        public NativeTypesLanguage TypesLanguage { get { return NativeTypesLanguage.FSharp; } }

        public string CodeFileExtension { get { return ".dtos.fs"; } }
    }

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
            return (baseUrl.WithTrailingSlash() + "types/" + TypesLanguage).BuildTypesUrlWithQueryStringValues(options);
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
    }

    public static class CommentParsingUtils
    {
        public static Dictionary<NativeTypesLanguage,string> StartCommentsSyntax = new Dictionary<NativeTypesLanguage, string>
        {
            { NativeTypesLanguage.CSharp, "/*" },
            { NativeTypesLanguage.FSharp, "(*" },
            { NativeTypesLanguage.VbNet, "'" }
        };

        public static Dictionary<NativeTypesLanguage, string> EndCommentsSyntax = new Dictionary<NativeTypesLanguage, string>
        {
            { NativeTypesLanguage.CSharp, "*/" },
            { NativeTypesLanguage.FSharp, "*)" },
            { NativeTypesLanguage.VbNet, "" }
        }; 

        public static List<string> ExtractFirstCommentBlock(this string codeOutput, NativeTypesLanguage langauge)
        {
            var allLines = codeOutput.ReadLines().ToList();
            int startOfBlockComment = 0;
            int endOfBlockComment = 0;

            //Find start of block comments
            for (int i = 0; i < allLines.Count; i++)
            {
                if (allLines[i].TrimStart().StartsWith(StartCommentsSyntax[langauge]))
                {
                    startOfBlockComment = i + 1;
                    break;
                }
            }

            for (int i = startOfBlockComment; i < allLines.Count; i++)
            {
                if (allLines[i].TrimStart().EqualsIgnoreCase(EndCommentsSyntax[langauge]))
                {
                    endOfBlockComment = i;
                    break;
                }
            }

            var commentLines = allLines.GetRange(startOfBlockComment, endOfBlockComment);
            return commentLines;
        }

        public static string BuildTypesUrlWithQueryStringValues(this string baseUrl, Dictionary<string, string> options)
        {
           string result = baseUrl;
            options = options ?? new Dictionary<string, string>(); 
            foreach (var option in options.Where(x => x.Key.ToLower() != "baseurl"))
            {
                result = result.AddQueryParam(option.Key, option.Value);
            }
            return result;
        }

        public static bool TryExtractBaseUrl(this INativeTypesHandler typesHandler, string codeFile, out string baseUrl)
        {
            baseUrl = "";
            var options = typesHandler.ParseComments(codeFile);
            if (options.ContainsKey("BaseUrl"))
            {
                baseUrl = options["BaseUrl"];
                return true;
            }
            return false;
        }
    }
}

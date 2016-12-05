using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace ServiceStackVS.NativeTypes
{
    public static class CommentParsingUtils
    {
        public static Dictionary<NativeTypesLanguage, string> StartCommentsSyntax = new Dictionary<NativeTypesLanguage, string>
        {
            { NativeTypesLanguage.CSharp, "/*" },
            { NativeTypesLanguage.FSharp, "(*" },
            { NativeTypesLanguage.VbNet, "'" },
            { NativeTypesLanguage.TypeScript, "/*" },
            { NativeTypesLanguage.TypeScriptConcrete, "/*" },
            { NativeTypesLanguage.Swift, "/*" },
            { NativeTypesLanguage.Java, "/*" },
            { NativeTypesLanguage.Kotlin, "/*" }
        };

        public static Dictionary<NativeTypesLanguage, string> EndCommentsSyntax = new Dictionary<NativeTypesLanguage, string>
        {
            { NativeTypesLanguage.CSharp, "*/" },
            { NativeTypesLanguage.FSharp, "*)" },
            { NativeTypesLanguage.VbNet, "" },
            { NativeTypesLanguage.TypeScript, "*/" },
            { NativeTypesLanguage.TypeScriptConcrete, "*/" },
            { NativeTypesLanguage.Swift, "*/" },
            { NativeTypesLanguage.Java, "*/" },
            { NativeTypesLanguage.Kotlin, "*/" },
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

            //Start from BaseUrl option, ignore Date,Version etc
            for (int i = startOfBlockComment; i < allLines.Count; i++)
            {
                if (allLines[i].TrimStart().ToLower().Equals("baseurl"))
                {
                    startOfBlockComment = i;
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

        public static bool TryExtractBaseUrl(this INativeTypesHandler typesHandler, string codeFromFile, out string baseUrl)
        {
            baseUrl = "";
            var options = typesHandler.ParseComments(codeFromFile);
            if (options.ContainsKey("BaseUrl"))
            {
                baseUrl = options["BaseUrl"];
                if (!string.IsNullOrEmpty(baseUrl))
                    return true;
            }
            return false;
        }
    }
}

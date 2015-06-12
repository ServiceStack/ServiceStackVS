using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStackVS.NativeTypes;

namespace ssutil_cli
{
    public class ProcessModeHandler
    {
        private static readonly List<INativeTypesHandler> SupportedNativeTypesHandlers = NativeTypeHandlers.All;


        public static CmdMode GetMode(Dictionary<string,string> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options","No options found.");
            }
            string url, filePath, lang;
            options.TryGetValue(UtilCliOptions.URL, out url);
            bool hasFilePath = options.TryGetValue(UtilCliOptions.FILE, out filePath);
            bool hasLang = options.TryGetValue(UtilCliOptions.LANG, out lang);

            if (!string.IsNullOrEmpty(url) && !hasFilePath && !hasLang)
            {
                //Add SS ref with default file name, "ServiceStackReference.dto.cs"
                return CmdMode.AddReference;
            }

            if (!string.IsNullOrEmpty(url) && hasFilePath && !hasLang)
            {
                //Fetch code from url, save at specified path with default lang (CSharp).
                return CmdMode.AddReferenceWithPath;
            }

            if (!string.IsNullOrEmpty(url) && hasFilePath && hasLang)
            {
                //Fetch code from url, save at specified path with specified lang file extension.
                return CmdMode.AddReferenceWithPathAndLang;
            }

            if (!string.IsNullOrEmpty(url) && !hasFilePath && hasLang)
            {
                //Fetch code from url, save with default file name at local path with specified lang file extension.
                return  CmdMode.AddRefWithLang;
            }

            if (string.IsNullOrEmpty(url) && hasFilePath && !string.IsNullOrEmpty(filePath))
            {
                //Fetch code from url, use BaseUrl and file extension to infer URL and language, save at specified path.
                return CmdMode.UpdateReference;
            }
            return CmdMode.Invalid;
        }

        public static void Process(Dictionary<string, string> options)
        {
            var mode = GetMode(options);
            
            switch (mode)
            {
                case CmdMode.Invalid:
                    //Do nothing
                    break;
                case CmdMode.AddReference:
                    ProcessAdd(options[UtilCliOptions.URL]);
                    break;
                case CmdMode.AddReferenceWithPath:
                    ProcessAdd(options[UtilCliOptions.URL], options[UtilCliOptions.FILE]);
                    break;
                case CmdMode.AddReferenceWithPathAndLang:
                    ProcessAdd(options[UtilCliOptions.URL], options[UtilCliOptions.FILE], options[UtilCliOptions.LANG]);
                    break;
                case CmdMode.AddRefWithLang:
                    ProcessAdd(options[UtilCliOptions.URL], null, options[UtilCliOptions.LANG]);
                    break;
                case CmdMode.UpdateReference:
                    ProcessUpdate(options[UtilCliOptions.FILE]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessAdd(string url, string path = null, string language = null)
        {
            INativeTypesHandler nativeTypesHandler = GetNativeTypesHandlerByLanguage(language);
            path = path ?? "ServiceStackReference" + nativeTypesHandler.CodeFileExtension;
            path = path.Contains(".") ? path : path + nativeTypesHandler.CodeFileExtension;
            string code;
            try
            {
                code = nativeTypesHandler.GetUpdatedCode(url, null);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get ServiceStack reference from '" + url + "'.");
                throw;
            }
            try
            {
                File.WriteAllText(path, code);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to write ServiceStack reference to '" + path + "'.");
                throw;
            }
        }

        private static INativeTypesHandler GetNativeTypesHandlerByLanguage(string lang)
        {
            lang = lang ?? "";
            switch (lang.ToLowerInvariant())
            {
                case "csharp":
                    return NativeTypeHandlers.CSharpNativeTypesHandler;
                case "fsharp":
                    return NativeTypeHandlers.FSharpNativeTypesHandler;
                case "typescript.d":
                    return NativeTypeHandlers.TypeScriptNativeTypesHandler;
                case "vbnet":
                    return NativeTypeHandlers.VbNetNativeTypesHandler;
                default:
                    if (lang != "")
                    {
                        Console.WriteLine("WARN: Invalid language '" + lang + "'. Defaulting to CSharp.");
                    }
                    return NativeTypeHandlers.CSharpNativeTypesHandler;
            }
        }

        private static void ProcessUpdate(string updateFilePath)
        {
            var nativeTypesHandler = ResolveNativeTypesHandlerByFilePath(updateFilePath);
            if (nativeTypesHandler == null)
            {
                throw new ArgumentException("Update file path provided does not have a valid ServiceStack reference extension.");
            }
            string updateFileCode;
            try
            {
                updateFileCode = File.ReadAllText(updateFilePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to read existing ServiceStack reference.");
                throw;
            }

            var updateOptions = nativeTypesHandler.ParseComments(updateFileCode);
            if (updateOptions == null || !updateOptions.ContainsKey("BaseUrl"))
            {
                throw new Exception("Update file is not valid or missing a BaseUrl");
            }

            string updatedCode;
            try
            {
                updatedCode = nativeTypesHandler.GetUpdatedCode(updateOptions["BaseUrl"], updateOptions);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to fetch updated code from '" + updateOptions["BaseUrl"] + "'.");
                throw;
            }
            try
            {
                File.WriteAllText(updateFilePath, updatedCode);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to write to update file path '" + updateFilePath + "'");
                throw;
            }
        }

        private static INativeTypesHandler ResolveNativeTypesHandlerByFilePath(string filePath)
        {
            return SupportedNativeTypesHandlers.FirstOrDefault(supportedNativeTypesHandler => supportedNativeTypesHandler.IsHandledFileType(filePath) );
        }
    }

    public enum CmdMode
    {
        Invalid,
        AddReference,
        AddReferenceWithPath,
        AddReferenceWithPathAndLang,
        AddRefWithLang,
        UpdateReference
    }
}

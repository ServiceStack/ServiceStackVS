using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Handlers;

namespace ssutil_cli
{
    public class ProcessModeHandler
    {

        public static readonly CSharpNativeTypesHandler CSharpNativeTypesHandler = new CSharpNativeTypesHandler();
        public static readonly FSharpNativeTypesHandler FSharpNativeTypesHandler = new FSharpNativeTypesHandler();
        public static readonly VbNetNativeTypesHandler VbNetNativeTypesHandler = new VbNetNativeTypesHandler();
        public static readonly TypeScriptNativeTypesHandler TypeScriptNativeTypesHandler = new TypeScriptNativeTypesHandler();
        public static readonly TypeScriptConcreteNativeTypesHandler TypeScriptConcreteNativeTypesHandler = new TypeScriptConcreteNativeTypesHandler();
        public static readonly JavaNativeTypesHandler JavaNativeTypesHandler = new JavaNativeTypesHandler();
        public static readonly SwitfNativeTypesHandler SwitfNativeTypesHandler = new SwitfNativeTypesHandler();
        public static readonly KotlinNativeTypesHandler KotlinNativeTypesHandler = new KotlinNativeTypesHandler();

        public static readonly Dictionary<string, INativeTypesHandler> All = new Dictionary<string,INativeTypesHandler>
        {
            {"csharp",CSharpNativeTypesHandler},
            {"fsharp",FSharpNativeTypesHandler},
            {"vbnet",VbNetNativeTypesHandler},
            {"typescript.d",TypeScriptNativeTypesHandler},
            {"typescript",TypeScriptConcreteNativeTypesHandler},
            {"java",JavaNativeTypesHandler},
            {"swift",SwitfNativeTypesHandler},
            {"kotlin",KotlinNativeTypesHandler}
        };

        public static CmdMode GetMode(Dictionary<string,string> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options","No options found.");
            }
            string urlOrFile, filePath, lang;
            options.TryGetValue(UtilCliOptions.DEFAULT, out urlOrFile);
            string url = "";
            string existingFile = "";
            if (File.Exists(urlOrFile))
            {
                existingFile = urlOrFile;
            }
            else
            {
                url = urlOrFile;
            }

            if (string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(existingFile))
            {
                //Fetch code from url, use BaseUrl and file extension to infer URL and language, save at specified path.
                return CmdMode.UpdateReference;
            }
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
                    ProcessAdd(options[UtilCliOptions.DEFAULT]);
                    break;
                case CmdMode.AddReferenceWithPath:
                    ProcessAdd(options[UtilCliOptions.DEFAULT], options[UtilCliOptions.FILE]);
                    break;
                case CmdMode.AddReferenceWithPathAndLang:
                    ProcessAdd(options[UtilCliOptions.DEFAULT], options[UtilCliOptions.FILE], options[UtilCliOptions.LANG]);
                    break;
                case CmdMode.AddRefWithLang:
                    ProcessAdd(options[UtilCliOptions.DEFAULT], null, options[UtilCliOptions.LANG]);
                    break;
                case CmdMode.UpdateReference:
                    ProcessUpdate(options[UtilCliOptions.DEFAULT]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessAdd(string url, string path = null, string language = null)
        {
            INativeTypesHandler nativeTypesHandler = GetNativeTypeHandler(language);
            
            path = path ?? "ServiceStackReference" + nativeTypesHandler.CodeFileExtension;
            bool fileNameContainsExtension = 
                path.LastIndexOf(".", StringComparison.Ordinal) > path.LastIndexOf(Path.DirectorySeparatorChar) &&
                (path.LastIndexOf(".", StringComparison.Ordinal) != path.LastIndexOf(Path.DirectorySeparatorChar));
            path = fileNameContainsExtension ? path : path + nativeTypesHandler.CodeFileExtension;
            
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

        private static INativeTypesHandler GetNativeTypeHandler(string lang = null)
        {
            INativeTypesHandler result = null;
            if (lang == null)
            {
                foreach (var nativeTypesHandlerPair in All)
                {
                    if (AppDomain.CurrentDomain.FriendlyName.Contains(nativeTypesHandlerPair.Key))
                    {
                        result = nativeTypesHandlerPair.Value;
                        break;
                    }
                }
                if (result == null)
                {
                    Console.WriteLine("WARN: Invalid language '" + lang + "'. Defaulting to CSharp.");
                    result = CSharpNativeTypesHandler;
                }
            }
            else if(All.ContainsKey(lang.ToLowerInvariant()))
            {
                result = All[lang.ToLowerInvariant()];
            }

            if (result == null)
            {
                Console.WriteLine("WARN: Invalid language '" + lang + "'. Defaulting to CSharp.");
            }

            return result ?? CSharpNativeTypesHandler;
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
            return All.Values.FirstOrDefault(supportedNativeTypesHandler => supportedNativeTypesHandler.IsHandledFileType(filePath) );
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

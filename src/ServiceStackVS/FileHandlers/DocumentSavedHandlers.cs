using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.NativeTypes;
using Task = System.Threading.Tasks.Task;

namespace ServiceStackVS.FileHandlers
{
    public class DocumentSavedHandlers
    {
        public static int MajorVisualStudioVersion
        {
            get { return int.Parse(Dte.Version.Substring(0, 2)); }
        }

        private static DTE2 _dte;
        public static DTE2 Dte
        {
            get { return _dte ?? (_dte = (DTE2)Package.GetGlobalService(typeof (DTE2))); }
        }

        private static readonly Dictionary<Predicate<Document>, Action<Document, OutputWindowWriter>> FileWatchers =
            new Dictionary<Predicate<Document>, Action<Document, OutputWindowWriter>>
            {
                //CSharp DTO
                {CSharpDtoPredicate, (doc, writer) => HandleDtoUpdate(doc,Dte, NativeTypeHandlers.CSharpNativeTypesHandler, writer)},
                //FSharp DTO
                {FSharpDtoPredicate, (doc, writer) => HandleDtoUpdate(doc,Dte, NativeTypeHandlers.FSharpNativeTypesHandler, writer)},
                //VbNet DTO
                {VbNetDtoPredicate, (doc, writer) => HandleDtoUpdate(doc,Dte, NativeTypeHandlers.VbNetNativeTypesHandler, writer)}
            };

        public static void HandleDocumentSaved(Document document, OutputWindowWriter windowWriter)
        {
            foreach (var filterWatcher in FileWatchers)
            {
                var predicate = filterWatcher.Key;
                var fileHandler = filterWatcher.Value;
                if (!predicate(document))
                {
                    continue;
                }
                fileHandler(document, windowWriter);
                break;
            }
        }

        public static bool CSharpDtoPredicate(Document document)
        {
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.CSharpNativeTypesHandler.CodeFileExtension)
                && !document.IsUpdateReferenceOnSaveDisabled();
        }

        public static bool FSharpDtoPredicate(Document document)
        {
            return (document.Name.EndsWithIgnoreCase(NativeTypeHandlers.FSharpNativeTypesHandler.CodeFileExtension) ||
                    //To support original naming of FSharp files
                   document.Name.EndsWithIgnoreCase(".dto.fs")) && !document.IsUpdateReferenceOnSaveDisabled();
        }

        public static bool VbNetDtoPredicate(Document document)
        {
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.VbNetNativeTypesHandler.CodeFileExtension)
                && !document.IsUpdateReferenceOnSaveDisabled();
        }

        private static void HandleDtoUpdate(Document document, DTE2 dte, INativeTypesHandler typesHandler,
            OutputWindowWriter outputWindowWriter)
        {
            string fullPath = document.ProjectItem.GetFullPath();
            outputWindowWriter.ShowOutputPane(dte);
            outputWindowWriter.Show();
            outputWindowWriter.WriteLine(
                "--- Updating ServiceStack Reference '" +
                fullPath.Substring(fullPath.LastIndexOf("\\", StringComparison.Ordinal) + 1) +
                "' ---");
            var existingGeneratedCode = File.ReadAllLines(fullPath).Join(Environment.NewLine);

            if (!typesHandler.TryExtractBaseUrl(existingGeneratedCode, out var baseUrl))
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unable to find BaseUrl");
                return;
            }
            try
            {
                var options = typesHandler.ParseComments(existingGeneratedCode);
                string updatedCode = typesHandler.GetUpdatedCode(baseUrl, options);

                try
                {
                    //Can't work out another way that ensures UI is updated.
                    //Overwriting the file inconsistently prompts the user that file has changed.
                    //Sometimes old code persists even though file has changed.
                    document.Close();
                }
                catch (Exception ex)
                {
                    outputWindowWriter.WriteLine("ServiceStack Reference: document.Close() - " + ex.Message);
                }

                try
                {
                    File.WriteAllText(fullPath, updatedCode);
                    bool optOutOfStats = Dte.GetOptOutStatsSetting();
                    if (!optOutOfStats)
                    {
                        var langName = typesHandler.RelativeTypesUrl.Substring(6);
                        Analytics.SubmitAnonymousUpdateReferenceUsage(langName);
                    }
                }
                catch (Exception ex)
                {
                    outputWindowWriter.WriteLine("ServiceStack Reference: File.WriteAllText() - " + ex.Message);
                }

                //HACK to ensure new file is loaded
                Task.Run(() =>
                {
                    try
                    {
                        var file = document.DTE.ItemOperations.OpenFile(fullPath);
                    }
                    catch (Exception e)
                    {
                        outputWindowWriter.WriteLine("ServiceStack Reference: document.DTE.ItemOperations.OpenFile() - " + e.Message);
                    }
                });
            }
            catch (Exception e)
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unhandled error - " + e.Message);
            }

            outputWindowWriter.WriteLine("--- Update ServiceStack Reference Complete ---");
        }
    }
}
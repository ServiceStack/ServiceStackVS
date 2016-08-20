using System;
using System.Collections.Generic;
using System.IO;
using EnvDTE;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NPMInstallerWizard;
using Task = System.Threading.Tasks.Task;

namespace ServiceStackVS.FileHandlers
{
    public class DocumentSavedHandlers
    {
        private static bool _hasBowerInstalled;
        private static bool _hasNpmInstalled;

        public static int MajorVisualStudioVersion
        {
            get { return int.Parse(Dte.Version.Substring(0, 2)); }
        }

        private static DTE _dte;
        public static DTE Dte
        {
            get { return _dte ?? (_dte = (DTE)Package.GetGlobalService(typeof (DTE))); }
        }

        private static IVsExtensionManager extensionManager;
        public static IVsExtensionManager ExtensionManager
        {
            get { return extensionManager ?? (extensionManager = (IVsExtensionManager)Package.GetGlobalService(typeof(SVsExtensionManager))); }
        }

        private static readonly Dictionary<Predicate<Document>, Action<Document, OutputWindowWriter>> FileWatchers =
            new Dictionary<Predicate<Document>, Action<Document, OutputWindowWriter>>
            {
                //NPM
                {NpmDocumentPredicate, NpmDocumentHandler},
                //Bower
                {BowerDocumentPredicate, BowerDocumentHandler},
                //CSharp DTO
                {CSharpDtoPredicate, (doc, writer) => HandleDtoUpdate(doc, NativeTypeHandlers.CSharpNativeTypesHandler, writer)},
                //FSharp DTO
                {FSharpDtoPredicate, (doc, writer) => HandleDtoUpdate(doc, NativeTypeHandlers.FSharpNativeTypesHandler, writer)},
                //VbNet DTO
                {VbNetDtoPredicate, (doc, writer) => HandleDtoUpdate(doc, NativeTypeHandlers.VbNetNativeTypesHandler, writer)}
            };

        private static bool NpmDocumentPredicate(Document document)
        {
            string path = document.GetProjectPath();
            return document.Name.EqualsIgnoreCase("package.json") && document.Path.EqualsIgnoreCase(path);
        }

        private static void NpmDocumentHandler(Document document, OutputWindowWriter windowWriter)
        {
            if (document.IsNpmUpdateDisable() || MajorVisualStudioVersion == 14 || (MajorVisualStudioVersion == 12 && ExtensionManager.HasExtension("Package Intellisense")))
            {
                return;
            }

            _hasNpmInstalled = _hasNpmInstalled
                ? _hasNpmInstalled
                : NodePackageUtils.TryRegisterNpmFromDefaultLocation();

            if (!_hasNpmInstalled)
            {
                windowWriter.Show();
                windowWriter.WriteLine("Node.js Installation not detected. Visit http://nodejs.org/ to download.");
                return;
            }
            document.TryRunNpmInstall(windowWriter);
        }

        private static bool BowerDocumentPredicate(Document document)
        {
            string path = document.GetProjectPath();
            return document.Name.EqualsIgnoreCase("bower.json") && document.Path.EqualsIgnoreCase(path);
        }

        private static void BowerDocumentHandler(Document document, OutputWindowWriter windowWriter)
        {
            if (document.IsBowerUpdateDisabled())
            {
                return;
            }

            _hasBowerInstalled = _hasBowerInstalled ? _hasBowerInstalled : NodePackageUtils.HasBowerOnPath();

            if (!_hasBowerInstalled)
            {
                windowWriter.Show();
                windowWriter.WriteLine(
                    "Bower Installation not detected. Run npm install bower -g to install if Node.js/NPM already installed.");
                return;
            }
            document.TryBowerInstall(windowWriter);
        }

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

        public static bool TypeScriptDtoPredicate(Document document)
        {
            return NativeTypeHandlers.TypeScriptNativeTypesHandler.IsHandledFileType(document.Name)
                && !document.IsUpdateReferenceOnSaveDisabled();
        }

        public static bool TypeScriptConcreteDtoPredicate(Document document)
        {
            return NativeTypeHandlers.TypeScriptConcreteNativeTypesHandler.IsHandledFileType(document.Name)
                   && !document.IsUpdateReferenceOnSaveDisabled();
        }

        private static void HandleDtoUpdate(Document document, INativeTypesHandler typesHandler,
            OutputWindowWriter outputWindowWriter)
        {
            string fullPath = document.ProjectItem.GetFullPath();
            outputWindowWriter.ShowOutputPane(document.DTE);
            outputWindowWriter.Show();
            outputWindowWriter.WriteLine(
                "--- Updating ServiceStack Reference '" +
                fullPath.Substring(fullPath.LastIndexOf("\\", StringComparison.Ordinal) + 1) +
                "' ---");
            var existingGeneratedCode = File.ReadAllLines(fullPath).Join(Environment.NewLine);
            string baseUrl;
            if (!typesHandler.TryExtractBaseUrl(existingGeneratedCode, out baseUrl))
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unabled to find BaseUrl");
                return;
            }
            try
            {
                var options = typesHandler.ParseComments(existingGeneratedCode);
                string updatedCode = typesHandler.GetUpdatedCode(baseUrl, options);

                //Can't work out another way that ensures UI is updated.
                //Overwriting the file inconsistently prompts the user that file has changed.
                //Sometimes old code persists even though file has changed.
                document.Close();
                using (var streamWriter = File.CreateText(fullPath))
                {
                    streamWriter.Write(updatedCode);
                    streamWriter.Flush();
                }
                try
                {
                    bool optOutOfStats = Dte.GetOptOutStatsSetting();
                    if (!optOutOfStats)
                    {
                        var langName = typesHandler.RelativeTypesUrl.Substring(6);
                        Analytics.SubmitAnonymousUpdateReferenceUsage(langName);
                    }
                }
                catch (Exception)
                {
                    // Ignore errors
                }
                //HACK to ensure new file is loaded
                Task.Run(() => { document.DTE.ItemOperations.OpenFile(fullPath); });
            }
            catch (Exception e)
            {
                outputWindowWriter.WriteLine("Failed to update ServiceStack Reference: Unhandled error - " + e.Message);
            }

            outputWindowWriter.WriteLine("--- Update ServiceStack Reference Complete ---");
        }
    }
}
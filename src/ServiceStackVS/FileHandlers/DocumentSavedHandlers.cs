using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using ServiceStack;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NPMInstallerWizard;
using ServiceStackVS.Wizards;

namespace ServiceStackVS.FileHandlers
{
    public class DocumentSavedHandlers
    {
        private static bool _hasBowerInstalled;
        private static bool _hasNpmInstalled;

        private static readonly Dictionary<Predicate<Document>, Action<Document, OutputWindowWriter>> FilterWatchers =
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
                {VbNetDtoPredicate, (doc, writer) => HandleDtoUpdate(doc, NativeTypeHandlers.VbNetNativeTypesHandler, writer)},
                //TypeScript DTO
                {TypeScriptDtoPredicate, (doc, writer) => HandleDtoUpdate(doc,NativeTypeHandlers.TypeScriptNativeTypesHandler, writer)}
            };

        private static bool NpmDocumentPredicate(Document document)
        {
            string path = document.GetProjectPath();
            return document.Name.EqualsIgnoreCase("package.json") && document.Path.EqualsIgnoreCase(path);
        }

        private static void NpmDocumentHandler(Document document, OutputWindowWriter windowWriter)
        {
            if (document.IsNpmUpdateDisable())
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
            foreach (var filterWatcher in FilterWatchers)
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
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.CSharpNativeTypesHandler.CodeFileExtension);
        }

        public static bool FSharpDtoPredicate(Document document)
        {
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.FSharpNativeTypesHandler.CodeFileExtension) ||
                    //To support original naming of FSharp files
                   document.Name.EndsWithIgnoreCase(".dto.fs");
        }

        public static bool VbNetDtoPredicate(Document document)
        {
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.VbNetNativeTypesHandler.CodeFileExtension);
        }

        public static bool TypeScriptDtoPredicate(Document document)
        {
            return document.Name.EndsWithIgnoreCase(NativeTypeHandlers.TypeScriptNativeTypesHandler.CodeFileExtension);            
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
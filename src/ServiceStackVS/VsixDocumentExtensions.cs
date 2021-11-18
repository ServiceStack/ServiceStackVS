using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnvDTE;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.FileHandlers;
using ServiceStackVS.NativeTypes;

namespace ServiceStackVS
{
    public static class VsixDocumentExtensions
    {

        public static List<INativeTypesHandler> GetTypeHandlerForSelectedFiles(this IList<SelectedItem> files)
        {
            if (files == null || files.Count == 0)
            {
                return new List<INativeTypesHandler>();
            }
            return files
                .Select(
                    file => NativeTypeHandlers.All.FirstOrDefault(handler => handler.IsHandledFileType(file.Name.ToLowerInvariant()))
                )
                .Where(handler => handler != null).ToList();
        }

        public static void HandleDocumentSaved(this Document document, OutputWindowWriter windowWriter)
        {
            DocumentSavedHandlers.HandleDocumentSaved(document, windowWriter);
        }

        public static bool IsUpdateReferenceOnSaveDisabled(this Document document)
        {
            string path = document.GetProjectPath();
            string settingsFilePath = Path.Combine(path, "servicestack.vsconfig");
            bool updateReferenceOnSaveDisabled = false;
            if (settingsFilePath.FileExists())
            {
                var settings = File.ReadAllText(settingsFilePath).ParseKeyValueText(" ");
                if (settings.TryGetValue("DisableUpdateReferenceOnSave", out var disableUpdateOnSave))
                {
                    updateReferenceOnSaveDisabled = disableUpdateOnSave.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
            }
            return updateReferenceOnSaveDisabled;
        }

        public static string GetProjectPath(this Document document)
        {
            string projectFile = document.ProjectItem.ContainingProject.FullName;
            string path = projectFile.Substring(0, projectFile.LastIndexOf("\\", StringComparison.Ordinal) + 1);
            return path;
        }

    }
}

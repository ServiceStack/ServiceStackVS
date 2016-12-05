using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Handlers;

namespace ServiceStackVS.NativeTypesWizard
{
    public class NativeTypesWizard : IWizard
    {
        public Dictionary<string, INativeTypesHandler> NativeTypesHandlers = new Dictionary<string, INativeTypesHandler>
        {
            {"csharp",new CSharpNativeTypesHandler()},
            {"fsharp",new FSharpNativeTypesHandler()},
            {"vbnet",new VbNetNativeTypesHandler()},
            {"typescript",new TypeScriptNativeTypesHandler()}
        };
        string userProviderItemName;
        private string finalUserProvidedName;
        private string finalProjectItemName;

        private INativeTypesHandler currentNativeTypesHandle;
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            
            userProviderItemName = replacementsDictionary["$safeitemname$"];
            if (runKind == WizardRunKind.AsNewItem)
            {
                string wizardData = replacementsDictionary["$wizarddata$"];
                XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
                var nativeTypeElement = element
                    .Descendants().FirstOrDefault(x => x.Name.LocalName.EqualsIgnoreCase("NativeType"));
                if (nativeTypeElement == null) return;

                var nativeTypeName = nativeTypeElement.Value;
                var typesHandler = NativeTypesHandlers.Where(x => x.Key.EqualsIgnoreCase(nativeTypeName)).Select(x => x.Value).FirstOrDefault();
                if (typesHandler == null)
                {
                    throw new WizardBackoutException("Unable to find associated INativeTypesHandler from '" + nativeTypeName + "'");
                }
                currentNativeTypesHandle = typesHandler;

                var dialog = new AddServiceStackReference(userProviderItemName, currentNativeTypesHandle);
                dialog.ShowDialog();
                if (!dialog.AddReferenceSucceeded)
                {
                    throw new WizardBackoutException("Cancelled");
                }
                finalUserProvidedName = dialog.FileNameTextBox.Text;
                replacementsDictionary.Add("$basereferenceurl$", dialog.ServerUrl.Replace(typesHandler.RelativeTypesUrl,string.Empty));
            }
            else
            {
                throw new WizardBackoutException("Invalid template runkind. Expecting 'WizardRunKind.AsNewItem'");
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            string projectPath = projectItem.ContainingProject.Properties.Item("FullPath").Value.ToString();
            
            if (!finalUserProvidedName.EndsWith(currentNativeTypesHandle.CodeFileExtension))
            {
                finalProjectItemName = MakeUniqueIfExists(Path.Combine(projectPath,
                           finalUserProvidedName + currentNativeTypesHandle.CodeFileExtension)).Name;
            }
            else
            {
                finalProjectItemName = MakeUniqueIfExists(Path.Combine(projectPath,
                                    finalUserProvidedName)).Name;
            }
            projectItem.Name = finalProjectItemName;
            projectItem.ContainingProject.Save();
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            projectItem.ContainingProject.Save();
            string projectPath = projectItem.ContainingProject.Properties.Item("FullPath").Value.ToString();
            string fullItemPath = Path.Combine(projectPath, finalProjectItemName);
            var existingGeneratedCode = File.ReadAllLines(fullItemPath).Join(Environment.NewLine);

            string baseUrl;
            if (!currentNativeTypesHandle.TryExtractBaseUrl(existingGeneratedCode, out baseUrl))
                throw new WizardBackoutException("Failed to read from template base url");

            try
            {
                string updatedCode = currentNativeTypesHandle.GetUpdatedCode(baseUrl, null);
                using (var streamWriter = File.CreateText(fullItemPath))
                {
                    streamWriter.Write(updatedCode);
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                OutputWindowWriter.WriterWindow.WriteLine("Failed to update ServiceStack Reference: Unhandled error - " + ex.Message);
            }

            projectItem.Open();
            projectItem.Save();
        }

        public void RunFinished()
        {
            
        }

        /// <summary>
        /// http://stackoverflow.com/a/1078016/670151
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileInfo MakeUniqueIfExists(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path).SplitOnFirst('.')[0];
            string fileExt = "." + Path.GetFileName(path).SplitOnFirst('.')[1];

            if (!File.Exists(path))
            {
                return new FileInfo(path);
            }
            for (int i = 1; ; ++i)
            {
                if (!File.Exists(path))
                    return new FileInfo(path);

                path = Path.Combine(dir, fileName + i + fileExt);
            }
        }
    }
}

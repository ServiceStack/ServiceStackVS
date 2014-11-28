using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using ServiceStack;
using ServiceStackVS.NativeTypes.Handlers;

namespace ServiceStackVS.NativeTypes
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
            if (!projectItem.Name.EndsWith(currentNativeTypesHandle.CodeFileExtension))
            {
                int fileNameNumber = 1;
                string projectPath = projectItem.ContainingProject.Properties.Item("FullPath").Value.ToString();
                //Find a version of the default name that doesn't already exist, 
                //mimicing VS default file name behaviour.
                while (File.Exists(Path.Combine(projectPath,
                    userProviderItemName + fileNameNumber + currentNativeTypesHandle.CodeFileExtension)))
                {
                    fileNameNumber++;
                }
                string suggestedFileName = userProviderItemName + fileNameNumber;
                var dialog = new AddServiceStackReference(suggestedFileName, currentNativeTypesHandle);
                dialog.ShowDialog();
                if (!dialog.AddReferenceSucceeded)
                {
                    return;
                }
                string templateCode = dialog.CodeTemplate;
                projectItem.Remove();
                string finalFileName = dialog.FileNameTextBox.Text + currentNativeTypesHandle.CodeFileExtension;
                string fullPath = Path.Combine(projectPath, finalFileName);
                using (var streamWriter = File.CreateText(fullPath))
                {
                    streamWriter.Write(templateCode);
                    streamWriter.Flush();
                }
                var project = projectItem.ContainingProject;
                var newDtoFile = project.ProjectItems.AddFromFile(fullPath);
                newDtoFile.Open(EnvDteConstants.vsViewKindCode);
                newDtoFile.Save();
                project.Save();
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            
        }

        public void RunFinished()
        {
            
        }
    }
}

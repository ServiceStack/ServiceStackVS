using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using ServiceStack;

namespace ServiceStackVS.ExternalTemplateWizard
{
    public class ExternalTemplateWizard : IWizard
    {
        private DTE2 dte;
        private Solution2 solution;

        private string templatesRootDir;
        private string externalTemplateDir;
        private string externalTemplateWizardData;
        private string solutionDir;
        private string externalTemplateName;
        private string externalProjectPath;
        private string externalSolutionPath;
        private string projectName;

        private string slnOutputName;
        private string projOutputName;
        private List<string> allTemplatedFiles = new List<string>();
        private Dictionary<string, string> replacementsDictionary;
        
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = automationObject as DTE2;
            if (dte != null) solution = (Solution2) dte.Solution;
            this.replacementsDictionary = replacementsDictionary;
            projectName = replacementsDictionary["$safeprojectname$"];
            templatesRootDir = Path.GetDirectoryName(customParams[0] as string);
            replacementsDictionary.Add("$saferootprojectname$", replacementsDictionary["$safeprojectname$"]);
            if (templatesRootDir == null)
            {
                throw new WizardBackoutException("Failed to create project, 'customParams' does not contain extension template path.");
            }
            solutionDir = Path.GetDirectoryName(replacementsDictionary["$destinationdirectory$"]);
            string wizardData = replacementsDictionary["$wizarddata$"];
            XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
            XElement externalTemplateRoot = null;
            if (element.Descendants().FirstOrDefault(x => x.Name.LocalName == "ExternalTemplate") != null)
            {
                externalTemplateRoot =
                    element.Descendants().First(x => x.Name.LocalName == "ExternalTemplate");
            }
            if (externalTemplateRoot == null)
            {
                return;
            }
            externalTemplateName = externalTemplateRoot.GetExternalTemplateName();
            externalTemplateDir = Path.Combine(templatesRootDir, externalTemplateName);
            externalSolutionPath = Path.Combine(externalTemplateDir,
                externalTemplateRoot.GetExternalTemplateSolutionFileName());
            externalProjectPath = Path.Combine(externalTemplateDir,
                externalTemplateRoot.GetExternalTemplateProjectFileName());
            allTemplatedFiles = externalTemplateRoot.GetListOfTemplatedFiles();

            slnOutputName = externalTemplateRoot.GetAttributeValue("outputSolutionName");
            projOutputName = externalTemplateRoot.GetAttributeValue("outputProjectName");
        }

        public void ProjectFinishedGenerating(Project project)
        {
            if (externalSolutionPath == null)
            {
                return;
            }
            //Create solution
            string solutionContents = File.ReadAllText(externalSolutionPath);
            File.WriteAllText(Path.Combine(solutionDir, slnOutputName.ReplaceAllTokens(replacementsDictionary)), solutionContents.ReplaceAllTokens(replacementsDictionary));

            //Create proj
            string projectContents = File.ReadAllText(externalProjectPath);
            string projectContainerPath = Path.Combine(solutionDir, projectName);
            var projContainerInfo = Directory.CreateDirectory(projectContainerPath);
            string projOutputNameResult = projOutputName.ReplaceAllTokens(replacementsDictionary);
            string projectContentsPath = Path.Combine(projContainerInfo.FullName,
                projOutputNameResult.Substring(0, projOutputNameResult.Length - 7));
            var projPath = Directory.CreateDirectory(projectContentsPath);
            File.WriteAllText(Path.Combine(projPath.FullName, projOutputName.ReplaceAllTokens(replacementsDictionary)), projectContents.ReplaceAllTokens(replacementsDictionary));

            //Create files

            foreach (var templatedFile in allTemplatedFiles)
            {
                string templateContents = File.ReadAllText(Path.Combine(externalTemplateDir, templatedFile));
                string resultContents = templateContents.ReplaceAllTokens(replacementsDictionary);
                File.WriteAllText(Path.Combine(projPath.FullName, templatedFile), resultContents);
            }
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            
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

    public static class ExternalTemplateUtils
    {
        public static string GetExternalTemplateName(this XElement element)
        {
            return GetAttributeValue(element,"name");
        }

        public static string GetExternalTemplateProjectFileName(this XElement element)
        {
            return GetAttributeValue(element, "projFile");
        }

        public static string GetExternalTemplateSolutionFileName(this XElement element)
        {
            return GetAttributeValue(element, "solutionFile");
        }

        public static List<string> GetListOfTemplatedFiles(this XElement element)
        {
            List<string> result = new List<string>();

            if (element.Descendants().Any(x => x.Name.LocalName == "Files") == false)
            {
                return result;
            }

            List<XElement> fileElements = element.Descendants().Where(x => x.Name.LocalName == "File").ToList();
            result = fileElements.Select(x => x.GetAttributeValue("name")).ToList();
            return result;
        }

        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            string result = null;
            if (element.HasAttributes && element.Attributes().Any(x => x.Name.LocalName == attributeName))
            {
                result = element.Attributes().First(x => x.Name.LocalName == attributeName).Value;
            }
            return result;
        }

        public static string ReplaceAllTokens(this string contents, Dictionary<string, string> replacementsDictionary)
        {
            string result = contents;
            foreach (var pair in replacementsDictionary)
            {
                result = result.Replace(pair.Key, pair.Value);
            }
            return result;
        }
    }
}

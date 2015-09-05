using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;

namespace ServiceStackVS.BuildConfigWizard
{
    public class BuildConfigWizard : IWizard
    {
        private DTE dte = null;
        private SolutionConfiguration solutionConfig;
        private SolutionContext projSolutionContext;
        private Solution solution;

        private string buildConfigName;
        private string buildPlatformName;
        private string projectFileExtension;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            dte = automationObject as DTE;
            solution = dte.Solution;
            string wizardData = replacementsDictionary["$wizarddata$"];
            XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
            XElement buildConfigRootElement = null;
            if (element.Descendants().FirstOrDefault(x => x.Name.LocalName == "BuildConfig") != null)
            {
                buildConfigRootElement =
                    element.Descendants().First(x => x.Name.LocalName == "BuildConfig");
            }
            if (buildConfigRootElement == null)
            {
                return;
            }

            buildConfigName = buildConfigRootElement.GetAttributeValue("configuration") ?? "Debug";
            buildPlatformName = buildConfigRootElement.GetAttributeValue("platform") ?? "Any CPU";
            projectFileExtension = buildConfigRootElement.GetAttributeValue("projectFileExtension") ?? "csproj";
        }

        public void ProjectFinishedGenerating(Project project)
        {
            foreach (SolutionConfiguration solConfig in solution.SolutionBuild.SolutionConfigurations)
            {
                foreach (SolutionContext solutionContext in solConfig.SolutionContexts)
                {
                    if (solutionContext.ProjectName.Contains(project.Name + projectFileExtension) &&
                        solutionContext.PlatformName == buildPlatformName &&
                        solutionConfig.Name == buildConfigName)
                    {
                        projSolutionContext = solutionContext;
                        solutionConfig = solConfig;
                    }
                }
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
            if (projSolutionContext != null)
            {
                solutionConfig.Activate();
                projSolutionContext.ShouldBuild = true;
                //Force save all?
                dte.ExecuteCommand("File.SaveAll");
            }
        }
    }

    public static class Extensions
    {
        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            string result = null;
            if (element.HasAttributes && element.Attributes().Any(x => x.Name.LocalName == attributeName))
            {
                result = element.Attributes().First(x => x.Name.LocalName == attributeName).Value;
            }
            return result;
        }
    }
}

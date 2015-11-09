using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace ServiceStackVS.NPMInstallerWizard
{
    public class NodeJsRequiredWizard : IWizard
    {
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsMultiProject)
            {
                if (!NodePackageUtils.TryRegisterNpmFromDefaultLocation())
                {
                    var form = new NodeJSInstallationPrompt();
                    form.ShowDialog();
                    if (!form.NodeFoundOnPath)
                    {
                        //Advise to restart VS and backout?
                        throw new WizardBackoutException("Node.js installation required");
                    }
                    
                }
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {

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
}

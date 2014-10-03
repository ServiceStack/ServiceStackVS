using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace ServiceStackVS.Wizards
{
    public class GitRequiredWizard : IWizard
    {
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsMultiProject)
            {
                if (!CommandUtils.TryRegisterAppFromProgramFiles("Git\\bin","git"))
                {
                    var form = new GitBashInstallationPrompt();
                    form.ShowDialog();
                    if (!form.GitFoundOnPath)
                    {
                        //Advise to restart VS and backout?
                        throw new WizardBackoutException("Git installation required");
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

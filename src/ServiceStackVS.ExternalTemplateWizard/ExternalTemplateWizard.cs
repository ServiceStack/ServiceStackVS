using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;

namespace ServiceStackVS.ExternalTemplateWizard
{
    public class ExternalTemplateWizard : IWizard
    {
        private DTE2 _dte;
        private string _templateDir;
        private string _solutionDir;
        private Solution2 _solution;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = automationObject as DTE2;
            if (_dte != null) _solution = (Solution2) _dte.Solution;
            _templateDir = System.IO.Path.GetDirectoryName(customParams[0] as string);
            _solutionDir = System.IO.Path.GetDirectoryName(replacementsDictionary["$destinationdirectory$"]);
        }

        public void ProjectFinishedGenerating(Project project)
        {
            throw new NotImplementedException();
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            throw new NotImplementedException();
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            throw new NotImplementedException();
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            throw new NotImplementedException();
        }

        public void RunFinished()
        {
            throw new NotImplementedException();
        }
    }
}

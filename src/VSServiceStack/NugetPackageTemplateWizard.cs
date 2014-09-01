using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet.VisualStudio;

namespace ServiceStackVS
{
    public class NuGetPackageInstallerWizard : IVsTemplateWizard
    {
        private IVsPackageInstaller _installer;
        private IVsPackageInstallerServices _packageServices;

        class PackageFromWizard
        {
            public string Id { get; set; }
            public string Version { get; set; }
        }

        private List<PackageFromWizard> PackagesToLoad { get; set; }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsNewProject)
            {
                _packageServices = (IVsPackageInstallerServices)Package.GetGlobalService(typeof(IVsPackageInstallerServices));
                _installer = (IVsPackageInstaller)Package.GetGlobalService(typeof(IVsPackageInstaller));
                string wizardData = replacementsDictionary["$wizarddata$"];
                XElement element = XElement.Parse(wizardData);
                var packages =
                    element.Descendants("package")
                        .Select(x => new PackageFromWizard { Id = x.Attribute("id").Value, Version = x.Attribute("version").Value });
                
                PackagesToLoad = new List<PackageFromWizard>(packages);
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            foreach (var packageFromWizard in PackagesToLoad)
            {
                AddNuGetDependencyIfMissing(project, packageFromWizard.Id);
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
            throw new NotImplementedException();
        }

        public void RunFinished()
        {
            throw new NotImplementedException();
        }

        private void AddNuGetDependencyIfMissing(Project project, string packageId)
        {
            //Once the generated code has been added, we need to ensure that  
            //the required ServiceStack.Interfaces package is installed.
            var installedPackages = _packageServices.GetInstalledPackages(project);

            //TODO check project references incase ServiceStack.Interfaces is referenced via local file.
            //VS has different ways to check different types of projects for refs, need to find method to check all.

            //Check if existing nuget reference exists
            if (installedPackages.FirstOrDefault(x => x.Id == packageId) == null)
            {
                _installer.InstallPackage("https://www.nuget.org/api/v2/",
                         project,
                         packageId,
                         version: (string)null, //Latest version of packageId
                         ignoreDependencies: false);
            }
        }
    }  
}

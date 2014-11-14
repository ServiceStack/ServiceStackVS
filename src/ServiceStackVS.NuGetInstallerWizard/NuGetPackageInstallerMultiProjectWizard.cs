using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ServiceStackVS.NuGetInstallerWizard
{
    public class NuGetPackageInstallerMultiProjectWizard : IWizard
    {
        public static NuGetWizardDataPackage RootNuGetPackage;
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsMultiProject)
            {
                using (var serviceProvider = new ServiceProvider((IServiceProvider)automationObject))
                {
                    var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
                    using (var container = new CompositionContainer(componentModel.DefaultExportProvider))
                    {
                        container.ComposeParts(this);
                    }
                }

                string wizardData = replacementsDictionary["$wizarddata$"];
                XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
                if (element.HasRootPackage())
                {
                    RootNuGetPackage = element.GetRootPackage();
                    RootNuGetPackage.Version = GetLatestVersionOfPackage(RootNuGetPackage.Id);
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

        private string GetLatestVersionOfPackage(string packageId)
        {
            IPackageRepository nugetV2Repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            var package = nugetV2Repository.FindPackagesById(packageId).First(x => x.IsLatestVersion);
            return package.Version.ToString();
        }
    }
}

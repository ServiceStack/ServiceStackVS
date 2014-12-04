using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
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

        private const string nugetV2Url = "https://packages.nuget.org/api/v2";

        private IPackageRepository nuGetPackageRepository;
        private IPackageRepository NuGetPackageRepository
        {
            get
            {
                return nuGetPackageRepository ??
                       (nuGetPackageRepository =
                           PackageRepositoryFactory.Default.CreateRepository(nugetV2Url));
            }
        }

        private IPackageRepository _cachedRepository;
        private IPackageRepository CachedRepository
        {
            get
            {
                string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string cachePath = Path.Combine(userAppData, "NuGet\\Cache");
                return _cachedRepository ??
                       (_cachedRepository = PackageRepositoryFactory.Default.CreateRepository(cachePath));
            }
        }

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
            try
            {
                var package = NuGetPackageRepository.FindPackagesById(packageId).First(x => x.IsLatestVersion);
                return package.Version.ToString();
            }
            catch (Exception)
            {
                var package = CachedRepository.FindPackagesById(packageId)
                    .OrderByDescending(x => x.Version.ToString())
                    .FirstOrDefault();
                if (package != null)
                {
                    return package.Version.ToString();
                }
                    
                throw new WizardBackoutException("Unable to connect to NuGet and no cached packages found for " + packageId);
            }
            
        }
    }
}

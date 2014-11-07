using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet;
using NuGet.VisualStudio;
using ServiceStack;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ServiceStackVS.Wizards
{
    public class NuGetPackageInstallerWizard : IWizard
    {
        [Import]
        internal IVsPackageInstaller Installer { get; set; }
        [Import]
        internal IVsPackageInstallerServices PackageServices { get; set; }

        class PackageFromWizard
        {
            public string Id { get; set; }
            public string Version { get; set; }
        }

        private List<PackageFromWizard> PackagesToLoad = new List<PackageFromWizard>();

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsNewProject)
            {
                using (var serviceProvider = new ServiceProvider((IServiceProvider) automationObject))
                {
                    var componentModel = (IComponentModel) serviceProvider.GetService(typeof (SComponentModel));
                    using (var container = new CompositionContainer(componentModel.DefaultExportProvider))
                    {
                        container.ComposeParts(this);
                    }
                }

                string wizardData = replacementsDictionary["$wizarddata$"];
                XElement element = XElement.Parse(wizardData);

                var packages =
                    element.Descendants()
                        .Select(
                            x =>
                                new PackageFromWizard
                                {
                                    Id = x.Attribute("id").Value,
                                    Version = x.Attribute("version").Value
                                });

                PackagesToLoad = new List<PackageFromWizard>(packages);
            }

        }

        public void ProjectFinishedGenerating(Project project)
        {
            foreach (var packageFromWizard in PackagesToLoad)
            {
                AddNuGetDependencyIfMissing(project, packageFromWizard.Id,packageFromWizard.Version);
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

        private void AddNuGetDependencyIfMissing(Project project, string packageId, string version = null)
        {
            var installedPackages = PackageServices.GetInstalledPackages(project);
            
            IPackageRepository nugetv2Service = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string cachePath = Path.Combine(userAppData, "..\\Local\\NuGet\\Cache");
            bool cacheExists = Directory.Exists(cachePath);
            if (cacheExists)
            {
                IPackageRepository localCache = PackageRepositoryFactory.Default.CreateRepository(cachePath);

                //Try use cache
                try
                {
                    List<IPackage> latestNugetPackages = nugetv2Service.FindPackagesById(packageId).ToList();
                    var latestNugetPackage = latestNugetPackages.FirstOrDefault(x => x.IsLatestVersion);
                    List<IPackage> latestCachePackages = nugetv2Service.FindPackagesById(packageId).ToList();
                    var latestCachePackage = latestCachePackages.FirstOrDefault(x => x.IsLatestVersion);
                    bool useCache = latestNugetPackage.Version == latestCachePackage.Version;
                    if (useCache)
                    {
                        Installer.InstallPackage(
                            cachePath, 
                            project, 
                            packageId,
                            version: latestCachePackage.Version.ToString(), //Null is latest version of packageId
                            ignoreDependencies: false);
                    }
                }
                catch (Exception)
                {
                    //Something went wrong with cache, fall back to NuGet

                }
            }
            
            version = string.IsNullOrEmpty(version) || version == "latest" ? null : version; //if empty or latest, set to null
            //Check if existing nuget reference exists
            if (installedPackages.FirstOrDefault(x => x.Id == packageId) == null)
            {
                Installer.InstallPackage("https://www.nuget.org/api/v2/",
                         project,
                         packageId,
                         version: version, //Null is latest version of packageId
                         ignoreDependencies: false);
            }
        }
    }  
}

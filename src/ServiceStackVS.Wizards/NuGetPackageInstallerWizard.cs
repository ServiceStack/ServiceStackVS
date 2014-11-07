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

        private static Dictionary<string, string> cachedLatestPackageVersions = new Dictionary<string, string>();
        private static bool nugetOnline = false;

        private const string OutputWindowGuid = "{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}";
        private const string ServiceStackVSPackageCmdSetGuid = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";

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
            var outputWindowPane = project.DTE.Windows.Item(OutputWindowGuid); //Output window pane
            var outputWindow = new OutputWindowWriter(ServiceStackVSPackageCmdSetGuid, "ServiceStackVS");
            

            foreach (var packageFromWizard in PackagesToLoad)
            {
                try
                {
                    AddNuGetDependencyIfMissing(project, packageFromWizard.Id, packageFromWizard.Version);
                }
                catch (Exception e)
                {
                    outputWindow.WriteLine("--- Failed to install ServiceStack NuGet dependencies ---");
                    outputWindow.WriteLine(e.Message);
                    outputWindowPane.Visible = true;
                    throw;
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
            
        }

        private void AddNuGetDependencyIfMissing(Project project, string packageId, string version = null)
        {
            if (TryInstallPackageFromCache(project, packageId, version)) return;

            var installedPackages = PackageServices.GetInstalledPackages(project);
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

        private bool TryInstallPackageFromCache(Project project, string packageId, string version)
        {
            string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string cachePath = Path.Combine(userAppData, "NuGet\\Cache");
            bool cacheExists = Directory.Exists(cachePath);
            bool useLatest = string.IsNullOrEmpty(version) || version == "latest";
            if (cacheExists && cachedLatestPackageVersions.ContainsKey(packageId))
            {
                Installer.InstallPackage(
                    cachePath,
                    project,
                    packageId,
                    version: useLatest ? null : cachedLatestPackageVersions[packageId], //Null is latest version of packageId
                    ignoreDependencies: false);
                return true;
            }

            List<IPackage> latestNugetPackages;
            IPackageRepository nugetV2Repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2"); ;
            IPackageRepository cachedRepository = PackageRepositoryFactory.Default.CreateRepository(cachePath);
            try
            {
                latestNugetPackages = nugetV2Repository.FindPackagesById(packageId).ToList();
            }
            catch (Exception)
            {
                //Nuget down or no connection
                //Try and revert to latest cached packages
                var latestCachePackage = cachedRepository.FindPackagesById(packageId)
                    .OrderByDescending(x => x.Version.ToString())
                    .FirstOrDefault(x => useLatest && x.IsLatestVersion || x.Version.ToString() == version);
                if (latestCachePackage == null)
                {
                    throw new WizardBackoutException("Failed to installed package from cache:" + packageId);
                }
                InstallPackageFromLocalCache(project, packageId, cachePath, latestCachePackage.Version.ToString());
                return true;
            }

            if (cacheExists)
            {

                var latestNugetPackage =
                    latestNugetPackages.FirstOrDefault(x => useLatest && x.IsLatestVersion || x.Version.ToString() == version);
                List<IPackage> latestCachePackages = cachedRepository.FindPackagesById(packageId).ToList();
                var latestCachePackage =
                    latestCachePackages.FirstOrDefault(x => useLatest && x.IsLatestVersion || x.Version.ToString() == version);
                bool useCache = latestCachePackage != null &&
                                latestNugetPackage != null &&
                                latestNugetPackage.Version == latestCachePackage.Version;
                if (useCache)
                {
                    InstallPackageFromLocalCache(project, packageId, cachePath, latestNugetPackage.Version.ToString());
                    return true;
                }

                if (latestNugetPackage == null)
                {
                    throw new ArgumentException("Invalid or unavailable version provided");
                }
            }
            return false;
        }

        private void InstallPackageFromLocalCache(Project project, string packageId, string cachePath, string version)
        {
            cachedLatestPackageVersions.Add(packageId, version);
            Installer.InstallPackage(
                cachePath,
                project,
                packageId,
                version: version,
                ignoreDependencies: false);
        }
    }  
}

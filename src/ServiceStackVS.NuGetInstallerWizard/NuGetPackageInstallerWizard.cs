using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet;
using NuGet.VisualStudio;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ServiceStackVS.NuGetInstallerWizard
{
    public class NuGetPackageInstallerWizard : IWizard
    {
        [Import]
        internal IVsPackageInstaller Installer { get; set; }
        [Import]
        internal IVsPackageInstallerServices PackageServices { get; set; }

        private const string ServiceStackVsOutputWindowPane = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";
        private OutputWindowWriter serviceStackOutputWindowWriter;
        private OutputWindowWriter OutputWindowWriter
        {
            get
            {
                return serviceStackOutputWindowWriter ?? 
                    (serviceStackOutputWindowWriter = new OutputWindowWriter(ServiceStackVsOutputWindowPane, "ServiceStackVS"));
            }
        }

        private List<NuGetWizardDataPackage> packagesToLoad = new List<NuGetWizardDataPackage>();

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
                XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
                packagesToLoad = element.ExtractNuGetPackages();
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            OutputWindowWriter.WriteLine("--- Installing latest ServiceStack NuGet dependencies for '" + project.Name + "' ---");
            foreach (var packageFromWizard in packagesToLoad)
            {
                try
                {
                    AddNuGetDependencyIfMissing(project, packageFromWizard.Id, packageFromWizard.Version);
                }
                catch (Exception e)
                {
                    OutputWindowWriter.WriteLine("--- Failed to install ServiceStack NuGet dependencies ---");
                    OutputWindowWriter.WriteLine(e.Message);
                    OutputWindowWriter.Show();
                    throw;
                }
            }
            OutputWindowWriter.WriteLine("--- Finished installing latest ServiceStack NuGet dependencies for '" + project.Name + "' ---");
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
                         version, //Null is latest version of packageId
                         ignoreDependencies: false);
            }
        }

        private bool TryInstallPackageFromCache(Project project, string packageId, string version)
        {
            string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string cachePath = Path.Combine(userAppData, "NuGet\\Cache");
            bool cacheExists = Directory.Exists(cachePath);
            bool useLatest = string.IsNullOrEmpty(version) || version == "latest";

            List<IPackage> latestNugetPackages;
            IPackageRepository nugetV2Repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            IPackageRepository cachedRepository = PackageRepositoryFactory.Default.CreateRepository(cachePath);
            try
            {
                latestNugetPackages = nugetV2Repository.FindPackagesById(packageId).ToList();
            }
            catch (Exception)
            {
                //Nuget down or no connection
                //Try and revert to latest cached packages
                OutputWindowWriter.WriteLine("--- WARNING: Unable to contact NuGet servers. Attempting to use local cache ---");
                var latestCachePackage = cachedRepository.FindPackagesById(packageId)
                    .OrderByDescending(x => x.Version.ToString())
                    .FirstOrDefault(x => useLatest && x.IsLatestVersion || x.Version.ToString() == version);
                if (latestCachePackage == null)
                {
                    OutputWindowWriter.WriteLine("--- ERROR: Unable to install ServiceStack from NuGet or local cache ---");
                    throw new WizardBackoutException("Failed to installed package from cache:" + packageId);
                }
                OutputWindowWriter.WriteLine("--- Installing " + packageId + " - " + latestCachePackage.Version + " from local cache ---");
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
                    OutputWindowWriter.WriteLine("--- Installing " + packageId + " - " + latestNugetPackage.Version + " ---");
                    InstallPackageFromLocalCache(project, packageId, cachePath, latestNugetPackage.Version.ToString());
                    return true;
                }

                if (latestNugetPackage == null)
                {
                    throw new ArgumentException("Invalid or unavailable version provided");
                }

                OutputWindowWriter.WriteLine("--- Installing " + packageId + " - " + latestNugetPackage.Version + " ---");
            }
            return false;
        }

        private void InstallPackageFromLocalCache(Project project, string packageId, string cachePath, string version)
        {
            Installer.InstallPackage(
                cachePath,
                project,
                packageId,
                version,
                ignoreDependencies: false);
        }
    }  
}

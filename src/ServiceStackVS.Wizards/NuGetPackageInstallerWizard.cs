using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
using NuGet.VisualStudio;
using DialogResult = Microsoft.Internal.VisualStudio.PlatformUI.DialogResult;
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

        private bool NpmInstalled;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (runKind == WizardRunKind.AsNewProject)
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
                XElement element = XElement.Parse(wizardData);
                bool promptUser = element.HasAttributes && element.Attribute("promptUser") != null &&
                                  element.Attribute("promptUser").Value == "true";
                if (promptUser)
                {
                    var dialogResult = MessageBox.Show("Do you want to install Node.JS and related dependencies locally?", "Node.JS Depedencies", MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        NpmInstalled = true;
                        var user = WindowsIdentity.GetCurrent();
                        if (user == null)
                        {
                            throw new Exception("Error creating template, no valid user identity");
                        }
                        // Get the built-in administrator account.
                        var sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid,
                            null);

                        // Compare to the current user.
                        bool isBuiltInAdmin = (user.User == sid);
                        if (!isBuiltInAdmin)
                        {
                            MessageBox.Show(
                                "Warning: This template requires the ability to set VS properties, without the correct permissions these will not persist. Run as Administrator to ensure these settings persist.",
                                "Template requires administrator rights", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
                
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            string foo = project.Globals["SSNpmInstalled"] = NpmInstalled.ToString();
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

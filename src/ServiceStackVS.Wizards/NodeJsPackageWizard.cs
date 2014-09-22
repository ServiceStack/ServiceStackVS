using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet.VisualStudio;
using ServiceStackVS.Wizards.Annotations;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ServiceStackVS.Wizards
{
    public class NodeJsPackageWizard : IWizard
    {
        private List<NpmPackage> npmPackages;

        private delegate void NpmPackageHandler(object sender, PackageInstallEventArgs eventArgs);
        private event NpmPackageHandler UpdateProgress;

        private NodeJsRequiredForm installationDialog;

        //private List<BowerPackage> bowerPackages; 

        /// <summary>
        /// Parses XML from WizardData and installs required npm packages
        /// </summary>
        /// <example>
        /// <![CDATA[
        /// <NodeJSRequirements requiresNpm="true">
        ///     <npm-package id="grunt"/>
        ///     <npm-package id="grunt-cli" />
        ///     <npm-package id="gulp" />
        ///     <npm-package id="bower" />
        /// </NodeJSRequirements>]]>
        /// </example>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            string wizardData = replacementsDictionary["$wizarddata$"];
            XElement element = XElement.Parse(wizardData);

            npmPackages =
                element.Descendants()
                    .Where(x => x.Name.LocalName == "npm-package")
                    .Select(x => new NpmPackage { Id = x.Attribute("id").Value })
                    .ToList();
            //Not needed
            //bowerPackages =
            //    element.Descendants()
            //        .Where(x => x.Name.LocalName == "bower-package")
            //        .Select(x => new BowerPackage {Id = x.Attribute("id").Value})
            //        .ToList();
            if (runKind == WizardRunKind.AsNewProject)
            {
                using (installationDialog = new NodeJsRequiredForm())
                {
                    UpdateProgress += installationDialog.UpdateProgress;
                    System.Threading.Tasks.Task.Run(() => StartRequiredPackageInstallations());
                    var result = installationDialog.ShowDialog();
                    if (!installationDialog.RequirementsMet)
                    {
                        throw new WizardBackoutException();
                    }
                }
            }
        }

        private void StartRequiredPackageInstallations()
        {
            try
            {
                System.Threading.Thread.Sleep(1000); //HACK Dialog not yet created, no method in form to override to fire.
                //Template required globally installed packages, eg bower to enable bower install
                foreach (var package in npmPackages)
                {
                    UpdateProgress.Invoke(this, new PackageInstallEventArgs { Package = package});
                    package.InstallGlobally(); //Installs global npm package if missing
                    UpdateProgress.Invoke(this, new PackageInstallEventArgs { Package = package, InstallationComplete = true});
                }
            }
            catch (ProcessException pe)
            {
                MessageBox.Show("An error has occurred during a NPM package installation - " + pe.Message,
                    "An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
            catch (TimeoutException te)
            {
                MessageBox.Show("An NPM install has timed out - " + te.Message,
                    "An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred during a NPM package installation." + e.Message,
                    "An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            string projectPath = project.FullName.Substring(0,
                project.FullName.LastIndexOf("\\", System.StringComparison.Ordinal));
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    if (!NodePackageUtils.HasBowerInPath())
                    {
                        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string npmFolder = Path.Combine(appDataFolder, "npm");
                        Environment.SetEnvironmentVariable("PATH",Environment.GetEnvironmentVariable("PATH") + ";" + npmFolder);
                    }
                    NodePackageUtils.RunBowerInstall(projectPath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Bower install failed: " + exception.Message,
                        "An error has occurred during a Bower install.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly,
                        false);
                    
                }
                
            }).Wait();
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    NodePackageUtils.NpmClearCache(projectPath);
                    NodePackageUtils.RunNpmInstall(projectPath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("NPM install failed: " + exception.Message,
                        "An error has occurred during an NPM install.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly,
                        false);
                    
                }
            });
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

    public class PackageInstallEventArgs : EventArgs
    {
        public NpmPackage Package { get; set; }
        public bool InstallationComplete { get; set; }
    }
}

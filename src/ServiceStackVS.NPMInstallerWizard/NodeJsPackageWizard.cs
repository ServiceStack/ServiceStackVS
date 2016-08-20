using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TemplateWizard;
using ServiceStack;
using ServiceStackVS.Common;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using Task = System.Threading.Tasks.Task;
using Thread = System.Threading.Thread;

namespace ServiceStackVS.NPMInstallerWizard
{
    public class NodeJsPackageWizard : IWizard
    {
        private const string ServiceStackVsOutputWindowPane = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";

        private IVsStatusbar bar;
        private DTE dte;
        private List<NpmPackage> npmPackages;

        private uint progressRef;

        private OutputWindowWriter serviceStackOutputWindowWriter;
        private IVsExtensionManager extensionManager;
        private bool skipTypings = false;

        private OutputWindowWriter OutputWindowWriter
        {
            get
            {
                return serviceStackOutputWindowWriter ??
                       (serviceStackOutputWindowWriter =
                           new OutputWindowWriter(ServiceStackVsOutputWindowPane, "ServiceStackVS"));
            }
        }

        [Import]
        public SVsServiceProvider ServiceProvider { get; set; }

        public IVsExtensionManager ExtensionManager
        {
            get { return extensionManager ?? (extensionManager = (IVsExtensionManager)Package.GetGlobalService(typeof(SVsExtensionManager))); }
        }

        public int MajorVisualStudioVersion
        {
            get { return int.Parse(dte.Version.Substring(0, 2)); }
        }

        private IVsStatusbar StatusBar
        {
            get { return bar ?? (bar = Package.GetGlobalService(typeof (SVsStatusbar)) as IVsStatusbar); }
        }

        /// <summary>
        ///     Parses XML from WizardData and installs required npm packages
        /// </summary>
        /// <example>
        ///     <![CDATA[
        /// <NodeJS>
        ///     <npm-package id="grunt"/>
        ///     <npm-package id="grunt-cli" />
        ///     <npm-package id="gulp" />
        ///     <npm-package id="bower" />
        /// </NodeJS>]]>
        /// </example>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            dte = (DTE) automationObject;

            using (var serviceProvider = new ServiceProvider((IServiceProvider)automationObject))
            {
                var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
                using (var container = new CompositionContainer(componentModel.DefaultExportProvider))
                {
                    container.ComposeParts(this);
                }
            }
            var wizardData = replacementsDictionary["$wizarddata$"];
            //HACK WizardData looks like root node, but not passed to this arg so we wrap it.
            //Problem is that only one SHARED WizardData node is supported and multiple wizards might use it.
            var element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
            npmPackages =
                element.Descendants()
                    .Where(x => x.Name.LocalName.EqualsIgnoreCase("npm-package"))
                    .Select(x => new NpmPackage {Id = x.Attribute("id").Value})
                    .ToList();
            var skipTypingsValue = element.Descendants()
                .Where(x => x.Name.LocalName.EqualsIgnoreCase("skip-typings-install"))
                .Select(x => x.Value)
                .FirstOrDefault();
            skipTypings = skipTypingsValue != null && skipTypingsValue == "true";

            if (NodePackageUtils.TryRegisterNpmFromDefaultLocation())
            {
                if (!NodePackageUtils.HasBowerOnPath())
                {
                    UpdateStatusMessage("Installing bower...");
                    NodePackageUtils.InstallNpmPackageGlobally("bower");
                }
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            var projectPath = project.FullName.Substring(0,
                project.FullName.LastIndexOf("\\", StringComparison.Ordinal));

            Task.Run(() => { StartRequiredPackageInstallations(); }).Wait();
            // Typings isn't supported by any built in VS features.. yet.., run manually and wait
            // This is due to problem with TSX intellisense which is fixed if project reloaded.
            // This is to ensure *.d.ts files are ready when template first loads
            Task.Run(() => { ProcessTypingsInstall(projectPath); }).Wait();
            //Only run Bower/NPM install via SSVS for VS 2012/2013
            //VS2015 built in Task Runner detects and runs required installs.
            //VS2013 Update 5 also does package restore on load.
            if (MajorVisualStudioVersion == 12 && !ExtensionManager.HasExtension("Package Intellisense") || MajorVisualStudioVersion == 11)
            {
                Task.Run(() => { ProcessBowerInstall(projectPath); }).Wait();

                UpdateStatusMessage("Downloading NPM depedencies...");
                OutputWindowWriter.ShowOutputPane(dte);

                Task.Run(() => { ProcessNpmInstall(projectPath); });
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

        private void StartRequiredPackageInstallations()
        {
            try
            {
                // Initialize the progress _bar.
                StatusBar.Progress(ref progressRef, 1, "", 0, 0);
                OutputWindowWriter.Show();
                for (var index = 0; index < npmPackages.Count; index++)
                {
                    var package = npmPackages[index];
                    UpdateStatusMessage("Installing required NPM package '" + package.Id + "'...");
                    package.InstallGlobally(
                        (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                OutputWindowWriter.WriteLine(s);
                            }
                        },
                        (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                OutputWindowWriter.WriteLine(s);
                            }
                        }); //Installs global npm package if missing
                    StatusBar.Progress(ref progressRef, 1, "", Convert.ToUInt32(index),
                        Convert.ToUInt32(npmPackages.Count));
                }
            }
            catch (ProcessException pe)
            {
                MessageBox.Show(@"An error has occurred during a NPM package installation - " + pe.Message,
                    @"An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
            catch (TimeoutException te)
            {
                MessageBox.Show(@"An NPM install has timed out - " + te.Message,
                    @"An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"An error has occurred during a NPM package installation." + e.Message,
                    @"An error has occurred during a NPM package installation.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
                throw new WizardBackoutException("An error has occurred during a NPM package installation.");
            }
        }

        private void UpdateStatusMessage(string message)
        {
            var frozen = 1;
            var retries = 0;
            while (frozen != 0 && retries < 10)
            {
                StatusBar.IsFrozen(out frozen);
                if (frozen == 0)
                {
                    StatusBar.SetText(message);
                }
                Thread.Sleep(10);
                retries++;
            }
        }

        private void ProcessBowerInstall(string projectPath)
        {
            try
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!File.Exists(Path.Combine(projectPath, "bower.json")))
                {
                    return;
                }
                if (!NodePackageUtils.HasBowerOnPath())
                {
                    var npmFolder = Path.Combine(appDataFolder, "npm");
                    npmFolder.AddToPathEnvironmentVariable();
                }
                UpdateStatusMessage("Downloading bower depedencies...");
                NodePackageUtils.RunBowerInstall(projectPath, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                        OutputWindowWriter.WriteLine(s);
                    }
                }, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                        OutputWindowWriter.WriteLine(s);
                    }
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Bower install failed: " + exception.Message,
                    @"An error has occurred during a Bower install.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
            }
        }

        private void ProcessTypingsInstall(string projectPath)
        {
            if (skipTypings)
                return;
            try
            {
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!File.Exists(Path.Combine(projectPath, "typings.json")))
                {
                    return;
                }
                if (!NodePackageUtils.HasTypingsOnPath())
                {

                    var npmFolder = Path.Combine(appDataFolder, "npm");
                    npmFolder.AddToPathEnvironmentVariable();
                }

                UpdateStatusMessage("Downloading typings depedencies...");
                NodePackageUtils.RunTypingsInstall(projectPath, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                        OutputWindowWriter.WriteLine(s);
                    }
                }, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                        OutputWindowWriter.WriteLine(s);
                    }
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Typings install failed: " + exception.Message,
                    @"An error has occurred during a Typings install.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly,
                    false);
            }
        }

        private void ProcessNpmInstall(string projectPath)
        {
            try
            {
                UpdateStatusMessage("Clearing NPM cache...");
                NodePackageUtils.NpmClearCache(projectPath);
                UpdateStatusMessage("Running NPM install...");
                OutputWindowWriter.WriteLine("--- NPM install started ---");
                NodePackageUtils.RunNpmInstall(projectPath,
                    (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                            OutputWindowWriter.WriteLine(s);
                        }
                    },
                    (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            var s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                            OutputWindowWriter.WriteLine(s);
                        }
                    }, 600);
                UpdateStatusMessage("Ready");
                StatusBar.Clear();
            }
            catch (Exception exception)
            {
                OutputWindowWriter.WriteLine("An error has occurred during an NPM install");
                OutputWindowWriter.WriteLine("NPM install failed: " + exception.Message);
            }
            OutputWindowWriter.WriteLine("--- NPM install complete ---");
        }
    }
}
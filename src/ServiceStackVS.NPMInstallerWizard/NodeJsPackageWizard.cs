using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TemplateWizard;
using ServiceStack;
using ServiceStackVS.Wizards;

namespace ServiceStackVS.NPMInstallerWizard
{
    public class NodeJsPackageWizard : IWizard
    {
        private const string ServiceStackVsOutputWindowPane = "5e5ab647-6a69-44a8-a2db-6a324b7b7e6d";
        private List<NpmPackage> _npmPackages;

        private OutputWindowWriter _serviceStackOutputWindowWriter;
        private OutputWindowWriter OutputWindowWriter
        {
            get
            {
                return _serviceStackOutputWindowWriter ??
                    (_serviceStackOutputWindowWriter = new OutputWindowWriter(ServiceStackVsOutputWindowPane, "ServiceStackVS"));
            }
        }

        public int MajorVisualStudioVersion
        {
            get { return int.Parse(_dte.Version.Substring(0, 2)); }
        }

        private uint _progressRef;

        private IVsStatusbar _bar;
        private DTE _dte;

        private IVsStatusbar StatusBar
        {
            get { return _bar ?? (_bar = Package.GetGlobalService(typeof (SVsStatusbar)) as IVsStatusbar); }
        }

        /// <summary>
        /// Parses XML from WizardData and installs required npm packages
        /// </summary>
        /// <example>
        /// <![CDATA[
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
            _dte = (DTE)automationObject;
            string wizardData = replacementsDictionary["$wizarddata$"];
            //HACK WizardData looks like root node, but not passed to this arg so we wrap it.
            //Problem is that only one SHARED WizardData node is supported and multiple extensions might use it.
            XElement element = XElement.Parse("<WizardData>" + wizardData + "</WizardData>");
            _npmPackages =
                element.Descendants()
                    .Where(x => x.Name.LocalName.EqualsIgnoreCase("npm-package"))
                    .Select(x => new NpmPackage {Id = x.Attribute("id").Value})
                    .ToList();

            if (NodePackageUtils.TryRegisterNpmFromDefaultLocation())
            {
                if (!NodePackageUtils.HasBowerOnPath())
                {
                    UpdateStatusMessage("Installing bower...");
                    NodePackageUtils.InstallNpmPackageGlobally("bower");
                }
            }
        }

        private void StartRequiredPackageInstallations()
        {
            try
            {
                // Initialize the progress _bar.
                StatusBar.Progress(ref _progressRef, 1, "", 0, 0);
                OutputWindowWriter.Show();
                for (int index = 0; index < _npmPackages.Count; index++)
                {
                    var package = _npmPackages[index];
                    UpdateStatusMessage("Installing required NPM package '" + package.Id + "'...");
                    package.InstallGlobally(
                        (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                OutputWindowWriter.WriteLine(s);
                            }
                        },
                        (sender, args) =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                                OutputWindowWriter.WriteLine(s);
                            }
                        }); //Installs global npm package if missing
                    StatusBar.Progress(ref _progressRef, 1, "", Convert.ToUInt32(index),
                        Convert.ToUInt32(_npmPackages.Count));
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
            int frozen = 1;
            int retries = 0;
            while (frozen != 0 && retries < 10)
            {
                StatusBar.IsFrozen(out frozen);
                if (frozen == 0)
                {
                    StatusBar.SetText(message);
                }
                System.Threading.Thread.Sleep(10);
                retries++;
            }
        }

        public void ProjectFinishedGenerating(Project project)
        {
            //Only run Bower/NPM insatll via SSVS for VS 2012/2013
            //VS2015 built in Task Runner detects and runs required installs.
            if (MajorVisualStudioVersion < 14)
            {
                string projectPath = project.FullName.Substring(0,
                    project.FullName.LastIndexOf("\\", StringComparison.Ordinal));
                System.Threading.Tasks.Task.Run(() => {
                    StartRequiredPackageInstallations();
                    ProcessBowerInstall(projectPath);
                }).Wait();

                UpdateStatusMessage("Downloading NPM depedencies...");
                OutputWindowWriter.ShowOutputPane(_dte);

                System.Threading.Tasks.Task.Run(() => {
                    ProcessNpmInstall(projectPath);
                });
            }
        }

        private void ProcessBowerInstall(string projectPath)
        {
            try
            {
                if (!NodePackageUtils.HasBowerOnPath())
                {
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string npmFolder = Path.Combine(appDataFolder, "npm");
                    npmFolder.AddToPathEnvironmentVariable();
                }
                UpdateStatusMessage("Downloading bower depedencies...");
                NodePackageUtils.RunBowerInstall(projectPath, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                        OutputWindowWriter.WriteLine(s);
                    }
                }, (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
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
                            string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
                            OutputWindowWriter.WriteLine(s);
                        }
                    },
                    (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            string s = Regex.Replace(args.Data, @"[^\u0000-\u007F]", string.Empty);
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
}
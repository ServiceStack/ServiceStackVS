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
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TemplateWizard;
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
        private bool skipTypings = false;
        private string projectName = null;

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

        public int MajorVisualStudioVersion
        {
            get
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
                return int.Parse(dte.Version.Substring(0, 2));
            }
        }

        private IVsStatusbar StatusBar
        {
            get { return bar ?? (bar = Package.GetGlobalService(typeof(SVsStatusbar)) as IVsStatusbar); }
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
            dte = (DTE)automationObject;

            projectName = replacementsDictionary["$safeprojectname$"];
            replacementsDictionary["$safeprojectnamelower$"] = projectName.ToLower();

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
                    .Where(x => x.Name.LocalName.Equals("npm-package", StringComparison.OrdinalIgnoreCase))
                    .Select(x => new NpmPackage { Id = x.Attribute("id").Value })
                    .ToList();
            var skipTypingsValue = element.Descendants()
                .Where(x => x.Name.LocalName.Equals("skip-typings-install", StringComparison.OrdinalIgnoreCase))
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

            //npm package.json validation requires lower-case names:
            var packageJsonPath = Path.Combine(projectPath, "package.json");
            if (projectName != null && File.Exists(packageJsonPath))
            {
                var originalContent = File.ReadAllText(packageJsonPath);
                var proectNameKebab = projectName.SplitCamelCase().Replace(" ", "-").ToLower();

                File.WriteAllText(packageJsonPath, originalContent
                    .ReplaceAll($"\"{projectName}\"", $"\"{proectNameKebab}\""));
            }

            ThreadHelper.JoinableTaskFactory.Run(async () => { await StartRequiredPackageInstallationsAsync(); });
            // Typings isn't supported by any built in VS features.. yet.., run manually and wait
            // This is due to problem with TSX intellisense which is fixed if project reloaded.
            // This is to ensure *.d.ts files are ready when template first loads
            ThreadHelper.JoinableTaskFactory.Run(async () => { await ProcessTypingsInstallAsync(projectPath); });
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

        private async Task StartRequiredPackageInstallationsAsync()
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
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

        private async Task ProcessTypingsInstallAsync(string projectPath)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
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
    }
    internal static class ServiceStackExtensions
    {
        private static readonly Regex SplitCamelCaseRegex = new Regex("([A-Z]|[0-9]+)", RegexOptions.Compiled);
        public static string SplitCamelCase(this string value) => SplitCamelCaseRegex.Replace(value, " $1").TrimStart();

        public static string ReplaceAll(this string haystack, string needle, string replacement)
        {
            int pos;
            // Avoid a possible infinite loop
            if (needle == replacement) return haystack;
            while ((pos = haystack.IndexOf(needle, StringComparison.Ordinal)) > 0)
            {
                haystack = haystack.Substring(0, pos)
                           + replacement
                           + haystack.Substring(pos + needle.Length);
            }
            return haystack;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;
using ServiceStack;
using ServiceStack.Text;
using ServiceStackVS.Types;
using ServiceStackVS.Wizards;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using MessageBox = System.Windows.MessageBox;
using Thread = System.Threading.Thread;

namespace ServiceStackVS
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVSServiceStackPkgString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    public sealed class ServiceStackVSPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public ServiceStackVSPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        private IComponentModel componentModel;
        public IComponentModel ComponentModel
        {
            get { return componentModel ?? (componentModel = (IComponentModel)GetService(typeof(SComponentModel))); }
        }

        private IVsPackageInstaller packageInstaller;
        public IVsPackageInstaller PackageInstaller
        {
            get { return packageInstaller ?? (packageInstaller = ComponentModel.GetService<IVsPackageInstaller>()); }
        }

        private IVsPackageInstallerServices pkgInstallerServices;

        public IVsPackageInstallerServices PackageInstallerServices
        {
            get
            {
                return pkgInstallerServices ??
                       (pkgInstallerServices = ComponentModel.GetService<IVsPackageInstallerServices>());
            }
        }

        private SolutionEventsListener solutionEventsListener;
        private OutputWindowWriter _outputWindow;

        private DocumentEvents _documentEvents;
        private ProjectItemsEvents _projectItemEvents;

        private DTE _dte;

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            packageInstaller = ComponentModel.GetService<IVsPackageInstaller>();
            pkgInstallerServices = ComponentModel.GetService<IVsPackageInstallerServices>();
            base.Initialize();

            _outputWindow = new OutputWindowWriter(this, GuidList.guidServiceStackVSOutputWindowPane, "ServiceStackVS");

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID cSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidServiceStackReference);
                var cSharpProjectContextOleMenuCommand = new OleMenuCommand(CSharpAddReferenceCallback ,cSharpProjContextAddReferenceCommandId);
                cSharpProjectContextOleMenuCommand.BeforeQueryStatus += CSharpQueryAndAddMenuItem;
                mcs.AddCommand(cSharpProjectContextOleMenuCommand);

                CommandID fSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpAddServiceStackReference);
                var fSharpProjectContextOleMenuCommand = new OleMenuCommand(FSharpAddReferenceCallback, fSharpProjContextAddReferenceCommandId);
                fSharpProjectContextOleMenuCommand.BeforeQueryStatus += FSharpQueryAndAddMenuItem;
                mcs.AddCommand(fSharpProjectContextOleMenuCommand);

                CommandID fSharpProjContextUpdateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpUpdateServiceStackReference);
                var fSharpProjectContextUpdateOleMenuCommand = new OleMenuCommand(FSharpUpdateReferenceCallback, fSharpProjContextUpdateReferenceCommandId);
                fSharpProjectContextUpdateOleMenuCommand.BeforeQueryStatus += FSharpQueryAndAddUpdateMenuItem;
                mcs.AddCommand(fSharpProjectContextUpdateOleMenuCommand);
            }

            solutionEventsListener = new SolutionEventsListener();
            solutionEventsListener.OnAfterOpenSolution += SolutionLoaded;
        }

        private void SolutionLoaded()
        {
            _dte = (DTE)GetService(typeof(DTE));
            if (_dte == null)
            {
                Debug.WriteLine("Unable to get the EnvDTE.DTE service.");
                return;
            }

            var events = _dte.Events as Events2;
            if (events == null)
            {
                Debug.WriteLine("Unable to get the Events2.");
                return;
            }

            _documentEvents = events.get_DocumentEvents();
            _documentEvents.DocumentSaved += DocumentEventsOnDocumentSaved;
        }

        private void CSharpQueryAndAddMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            Project project = VSIXUtils.GetSelectedProject();

            command.Visible = 
                project != null &&
                project.Kind != null && 
                string.Equals(project.Kind, "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}",
                    StringComparison.InvariantCultureIgnoreCase);

            command.Enabled =
                //Not busy building
                ready &&
                project != null &&
                project.Kind != null &&
                //Project is not unloaded
                !string.Equals(project.Kind, "{67294A52-A4F0-11D2-AA88-00C04F688DDE}",
                    StringComparison.InvariantCultureIgnoreCase) &&
                //Project is Csharp project
                string.Equals(project.Kind, "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}",
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private void FSharpQueryAndAddMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            Project project = VSIXUtils.GetSelectedProject();

            command.Enabled = ready &&
                              project != null &&
                              project.Kind != null &&
                //Project is not unloaded
                              !string.Equals(project.Kind, "{67294A52-A4F0-11D2-AA88-00C04F688DDE}",
                                  StringComparison.InvariantCultureIgnoreCase);
            command.Visible =
                 project != null &&
                project.Kind != null &&
                //Project is FSharp project
                string.Equals(project.Kind, "{F2A71F9B-5D33-465A-A702-920D77279786}",
                    StringComparison.InvariantCultureIgnoreCase);
        }

        private void FSharpQueryAndAddUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            ProjectItem projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            bool selectedFSharpDto = selectedFiles.Any((item) => item.Name.ToLowerInvariant().EndsWith(".dto.fs"));

            command.Enabled = ready && selectedFSharpDto &&
                              projectItem.Kind != null &&
                //Project is not unloaded
                              !string.Equals(projectItem.ContainingProject.Kind, "{67294A52-A4F0-11D2-AA88-00C04F688DDE}",
                                  StringComparison.InvariantCultureIgnoreCase);
            command.Visible = 
                selectedFSharpDto &&
                projectItem.Kind != null &&
                //Project is FSharp project
                string.Equals(projectItem.ContainingProject.Kind, "{F2A71F9B-5D33-465A-A702-920D77279786}",
                    StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void CSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var t4TemplateBase = Resources.ServiceModelTemplate;
            var project = VSIXUtils.GetSelectedProject();
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            int fileNameNumber = 1;
            //Find a version of the default name that doesn't already exist, 
            //mimicing VS default file name behaviour.
            while (File.Exists(Path.Combine(projectPath, "ServiceReference" + fileNameNumber + ".tt")))
            {
                fileNameNumber++;
            }
            var dialog = new AddServiceStackReference("ServiceReference" + fileNameNumber);
            dialog.UseCSharpProvider(t4TemplateBase);
            dialog.ShowDialog();
            if (!dialog.AddReferenceSucceeded)
            {
                return;
            }
            string templateCode = dialog.CodeTemplate;
            CreateAndAddTemplateToProject(dialog.FileNameTextBox.Text + ".tt", templateCode);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void FSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var project = VSIXUtils.GetSelectedProject();
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            int fileNameNumber = 1;
            //Find a version of the default name that doesn't already exist, 
            //mimicing VS default file name behaviour.
            while (File.Exists(Path.Combine(projectPath, "ServiceReference" + fileNameNumber + ".dto.fs")))
            {
                fileNameNumber++;
            }
            var dialog = new AddServiceStackReference("ServiceReference" + fileNameNumber);
            dialog.UseFSharpProvider();
            dialog.ShowDialog();
            if (!dialog.AddReferenceSucceeded)
            {
                return;
            }
            string templateCode = dialog.CodeTemplate;
            CreateAndAddTemplateToProject(dialog.FileNameTextBox.Text + ".dto.fs", templateCode);
        }

        private void FSharpUpdateReferenceCallback(object sender, EventArgs e)
        {
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
            _outputWindow.Show();
            _outputWindow.WriteLine("--- Updating ServiceStack Reference '" + projectItem.Name + "' ---");
            string projectItemPath = projectItem.Properties.Item("FullPath").Value.ToString();
            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>().ToList();
            bool selectedFSharpDto = selectedFiles.Any((item) => item.Name.ToLowerInvariant().EndsWith(".dto.fs"));
            if (selectedFSharpDto)
            {
                string filePath = projectItemPath;
                var fSharpCodeAllLines = File.ReadAllLines(filePath).ToList();
                string lineWithBaseUrl = fSharpCodeAllLines.FirstOrDefault(x => x.StartsWithIgnoreCase("BaseUrl: "));
                if (lineWithBaseUrl == null)
                {
                    throw new Exception("Unable to read URL from DTO file.");
                }
                string baseUrl =
                    lineWithBaseUrl.Substring(lineWithBaseUrl.IndexOf(" ", System.StringComparison.Ordinal)).Trim();
                string appendFSharpPath = "types/fsharp";
                string fSharpUrl = baseUrl.EndsWith("/") ? baseUrl + appendFSharpPath : baseUrl + "/" + appendFSharpPath;
                string updatedCode = new WebClient().DownloadString(fSharpUrl);
                using (var streamWriter = File.CreateText(filePath))
                {
                    streamWriter.Write(updatedCode);
                    streamWriter.Flush();
                }
                _outputWindow.WriteLine("--- Update ServiceStack Reference Complete ---");
            }
        }

        private void CreateAndAddTemplateToProject(string fileName, string templateCode)
        {
            var project = VSIXUtils.GetSelectedProject();
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            string fullPath = Path.Combine(projectPath, fileName);
            using (var streamWriter = File.CreateText(fullPath))
            {
                streamWriter.Write(templateCode);
                streamWriter.Flush();
            }
            var t4TemplateProjectItem = project.ProjectItems.AddFromFile(fullPath);
            t4TemplateProjectItem.Open(EnvDTE.Constants.vsViewKindCode);
            t4TemplateProjectItem.Save();

            AddNuGetDependencyIfMissing(project, "ServiceStack.Client");
            AddNuGetDependencyIfMissing(project, "ServiceStack.Text");
            project.Save();
        }

        private void AddNuGetDependencyIfMissing(Project project,string packageId)
        {
            //Once the generated code has been added, we need to ensure that  
            //the required ServiceStack.Interfaces package is installed.
            var installedPackages = PackageInstallerServices.GetInstalledPackages(project);

            //TODO check project references incase ServiceStack.Interfaces is referenced via local file.
            //VS has different ways to check different types of projects for refs, need to find method to check all.

            //Check if existing nuget reference exists
            if (installedPackages.FirstOrDefault(x => x.Id == packageId) == null)
            {
                PackageInstaller.InstallPackage("https://www.nuget.org/api/v2/",
                         project,
                         packageId,
                         version: (string)null, //Latest version of packageId
                         ignoreDependencies: false);
            }
        }

        private void DocumentEventsOnDocumentSaved(Document document)
        {
            //Check if document is package.json and not disabled by settings
            document.HandleNpmPackageUpdate(_outputWindow);
          
            //Check if document is bower.json and not disabled by settings
            document.HandleBowerPackageUpdate(_outputWindow);
        }
    }
}

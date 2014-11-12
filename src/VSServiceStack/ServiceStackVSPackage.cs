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
using ServiceStackVS.NPMInstallerWizard;
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
                CommandID cSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidCSharpAddServiceStackReference);
                var cSharpProjectAddReferenceMenuCommand = new OleMenuCommand(CSharpAddReferenceCallback ,cSharpProjContextAddReferenceCommandId);
                cSharpProjectAddReferenceMenuCommand.BeforeQueryStatus += CSharpQueryAddMenuItem;
                mcs.AddCommand(cSharpProjectAddReferenceMenuCommand);

                CommandID cSharpProjContextUpdateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidCSharpUpdateServiceStackReference);
                var cSharpProjectUpdateReferenceMenuCommand = new OleMenuCommand(CSharpUpdateReferenceCallback,
                    cSharpProjContextUpdateReferenceCommandId);
                cSharpProjectUpdateReferenceMenuCommand.BeforeQueryStatus += CSharpQueryUpdateMenuItem;
                mcs.AddCommand(cSharpProjectUpdateReferenceMenuCommand);

                CommandID fSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpAddServiceStackReference);
                var fSharpProjectContextOleMenuCommand = new OleMenuCommand(FSharpAddReferenceCallback, fSharpProjContextAddReferenceCommandId);
                fSharpProjectContextOleMenuCommand.BeforeQueryStatus += FSharpQueryAddMenuItem;
                mcs.AddCommand(fSharpProjectContextOleMenuCommand);

                CommandID fSharpProjContextUpdateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpUpdateServiceStackReference);
                var fSharpProjectContextUpdateOleMenuCommand = new OleMenuCommand(FSharpUpdateReferenceCallback, fSharpProjContextUpdateReferenceCommandId);
                fSharpProjectContextUpdateOleMenuCommand.BeforeQueryStatus += FSharpQueryUpdateMenuItem;
                mcs.AddCommand(fSharpProjectContextUpdateOleMenuCommand);

                CommandID vbNetProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidVbNetAddServiceStackReference);
                var vbNetProjectContextOleMenuCommand = new OleMenuCommand(VbNetAddReferenceCallback, vbNetProjContextAddReferenceCommandId);
                vbNetProjectContextOleMenuCommand.BeforeQueryStatus += VbNetQueryAddMenuItem;
                mcs.AddCommand(vbNetProjectContextOleMenuCommand);

                CommandID vbNetProjContextUpdateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidVbNetUpdateServiceStackReference);
                var vbNetProjectContextUpdateOleMenuCommand = new OleMenuCommand(VbNetUpdateReferenceCallback, vbNetProjContextUpdateReferenceCommandId);
                vbNetProjectContextUpdateOleMenuCommand.BeforeQueryStatus += VbNetQueryUpdateMenuItem;
                mcs.AddCommand(vbNetProjectContextUpdateOleMenuCommand);

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

        private void CSharpQueryAddMenuItem(object sender, EventArgs eventArgs)
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

            bool visible =
                project != null &&
                project.Kind != null &&
                //Project is not unloaded
                !string.Equals(project.Kind, VsHelperGuids.ProjectUnloaded,
                    StringComparison.InvariantCultureIgnoreCase) &&
                //Project is Csharp project
                string.Equals(project.Kind, VsHelperGuids.CSharpProjectKind,
                    StringComparison.InvariantCultureIgnoreCase);
                //Not busy building

            bool enabled = ready && visible;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        private void CSharpQueryUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var typesHandler = new CSharpNativeTypesHandler();
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            //Not busy building
            var ready = result == VSConstants.S_OK && pfActive > 0;
            ProjectItem projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            bool dtoSelected = selectedFiles.Any(
                (item) =>
                    item.Name.ToLowerInvariant().EndsWith(typesHandler.CodeFileExtension));
            bool visible = projectItem.ContainingProject != null &&
                           projectItem.ContainingProject.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase) &&
                           //Project is Csharp project
                           string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.CSharpProjectKind,
                               StringComparison.InvariantCultureIgnoreCase);

            bool enabled = ready && dtoSelected && visible;

            command.Visible = visible;
            command.Enabled = enabled;
            
        }

        private void FSharpQueryAddMenuItem(object sender, EventArgs eventArgs)
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

            bool visible = project != null &&
                           project.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(project.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase) &&
                           //Project is FSharp project
                           string.Equals(project.Kind, VsHelperGuids.FSharpProjectKind,
                               StringComparison.InvariantCultureIgnoreCase);

            bool enabled = visible && ready;

            command.Visible = visible;   
            command.Enabled = enabled;
        }

        private void FSharpQueryUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var typesHandlers = new FSharpNativeTypesHandler();
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            ProjectItem projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            //Alternate file extension for FSharp due to initial extension version looking for '.dto.fs', default is now '.dtos.*'
            bool dtoSelected = selectedFiles.Any(
                (item) =>
                    item.Name.ToLowerInvariant().EndsWith(typesHandlers.CodeFileExtension) ||
                    item.Name.ToLowerInvariant().EndsWith(".dto.fs"));
            bool visible = projectItem.ContainingProject != null &&
                           projectItem.ContainingProject.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase) &&
                           //Project is FSharp project
                           string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.FSharpProjectKind,
                               StringComparison.InvariantCultureIgnoreCase);
            bool enabled = visible && ready && dtoSelected;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        private void VbNetQueryAddMenuItem(object sender, EventArgs eventArgs)
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

            bool visible = project != null &&
                           project.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(project.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase) &&
                           //Project is VbNet project
                           string.Equals(project.Kind, VsHelperGuids.VbNetProjectKind,
                               StringComparison.InvariantCultureIgnoreCase);

            bool enabled = visible && ready;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        private void VbNetQueryUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            OleMenuCommand command = (OleMenuCommand)sender;
            var typesHandler = new VbNetNativeTypesHandler();
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            Guid guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            ProjectItem projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            bool dtoSelected =
                selectedFiles.Any((item) => item.Name.ToLowerInvariant().EndsWith(typesHandler.CodeFileExtension));
            bool visible = projectItem.ContainingProject != null &&
                           projectItem.ContainingProject.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase) &&
                           //Project is VbNet project
                           string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.VbNetProjectKind,
                               StringComparison.InvariantCultureIgnoreCase);
            bool enabled = visible && ready && dtoSelected;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void CSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var project = VSIXUtils.GetSelectedProject();
            var typesHandler = new CSharpNativeTypesHandler();
            AddServiceStackReference(project, typesHandler);
        }

        private void CSharpUpdateReferenceCallback(object sender, EventArgs e)
        {
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
            var typesHandler = new CSharpNativeTypesHandler();
            UpdateGeneratedDtos(projectItem, typesHandler);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void FSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var project = VSIXUtils.GetSelectedProject();
            var typesHandler = new FSharpNativeTypesHandler();
            AddServiceStackReference(project, typesHandler);
        }

        private void FSharpUpdateReferenceCallback(object sender, EventArgs e)
        {
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
            var typesHandler = new FSharpNativeTypesHandler();
            UpdateGeneratedDtos(projectItem, typesHandler);
        }

        private void VbNetAddReferenceCallback(object sender, EventArgs e)
        {
            var project = VSIXUtils.GetSelectedProject();
            var typesHandler = new VbNetNativeTypesHandler();
            AddServiceStackReference(project, typesHandler);
        }

        private void VbNetUpdateReferenceCallback(object sender, EventArgs e)
        {
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
            var typesHandler = new VbNetNativeTypesHandler();
            UpdateGeneratedDtos(projectItem, typesHandler);
        }

        private void AddServiceStackReference(Project project, INativeTypesHandler typesHandler)
        {
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            int fileNameNumber = 1;
            //Find a version of the default name that doesn't already exist, 
            //mimicing VS default file name behaviour.
            while (File.Exists(Path.Combine(projectPath, "ServiceReference" + fileNameNumber + typesHandler.CodeFileExtension)))
            {
                fileNameNumber++;
            }
            var dialog = new AddServiceStackReference("ServiceReference" + fileNameNumber, typesHandler);
            dialog.ShowDialog();
            if (!dialog.AddReferenceSucceeded)
            {
                return;
            }
            string templateCode = dialog.CodeTemplate;
            AddNewDtoFileToProject(dialog.FileNameTextBox.Text + typesHandler.CodeFileExtension, templateCode);
        }

        private void UpdateGeneratedDtos(ProjectItem projectItem, INativeTypesHandler typesHandler)
        {
            _outputWindow.Show();
            _outputWindow.WriteLine("--- Updating ServiceStack Reference '" + projectItem.Name + "' ---");
            string projectItemPath = projectItem.Properties.Item("FullPath").Value.ToString();
            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>().ToList();
            bool selectedFSharpDto = selectedFiles.Any((item) => item.Name.ToLowerInvariant().EndsWith(typesHandler.CodeFileExtension));
            if (selectedFSharpDto)
            {
                string filePath = projectItemPath;
                var existingGeneratedCode = File.ReadAllLines(filePath).Join(Environment.NewLine);

                string baseUrl = "";
                if (!typesHandler.TryExtractBaseUrl(existingGeneratedCode, out baseUrl))
                {
                    _outputWindow.WriteLine("Unable to read URL from DTO file. Please ensure the file was generated correctly from a ServiceStack server.");
                    return;
                }

                var options = typesHandler.ParseComments(existingGeneratedCode);
                string updatedCode = typesHandler.GetUpdatedCode(baseUrl, options);
                using (var streamWriter = File.CreateText(filePath))
                {
                    streamWriter.Write(updatedCode);
                    streamWriter.Flush();
                }
                _outputWindow.WriteLine("--- Update ServiceStack Reference Complete ---");
            }
            else
            {
                _outputWindow.WriteLine("--- Valid file not found ServiceStack Reference '" + projectItem.Name + "' ---");
            }
        }

        private void AddNewDtoFileToProject(string fileName, string templateCode)
        {
            var project = VSIXUtils.GetSelectedProject();
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            string fullPath = Path.Combine(projectPath, fileName);
            using (var streamWriter = File.CreateText(fullPath))
            {
                streamWriter.Write(templateCode);
                streamWriter.Flush();
            }
            var newDtoFile = project.ProjectItems.AddFromFile(fullPath);
            newDtoFile.Open(EnvDTE.Constants.vsViewKindCode);
            newDtoFile.Save();
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

            document.HandleCSharpDtoUpdate(_outputWindow);

            document.HandleFSharpDtoUpdate(_outputWindow);

            document.HandleVbNetDtoUpdate(_outputWindow);
        }
    }
}

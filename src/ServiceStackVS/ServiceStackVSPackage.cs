using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Reflection;
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
using ServiceStackVS.Common;
using ServiceStackVS.FileHandlers;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Handlers;
using ServiceStackVS.NativeTypesWizard;
using ServiceStackVS.NPMInstallerWizard;
using ServiceStackVS.Settings;
using VSLangProj;
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
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [ProvideOptionPage(typeof(ServiceStackOptionsDialogGrid),
    "ServiceStack", "General", 0, 0, true)]
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

        public readonly Dictionary<int, List<string>> TemplateCleanupByVsVersion = new Dictionary<int, List<string>>
                {
                    {11,new List<string> { "Aurelia.csharp", "ReactJSGap.csharp", "TypeScriptReact.csharp","Angular2.csharp" } },
                    {12,new List<string> { "Aurelia.csharp","Angular2.csharp" }},
                    {14,new List<string>()}
                };

        private IComponentModel componentModel;
        public IComponentModel ComponentModel => 
            componentModel ?? (componentModel = (IComponentModel)GetService(typeof(SComponentModel)));

        private IVsPackageInstaller packageInstaller;

        public IVsPackageInstaller PackageInstaller => 
            packageInstaller ?? (packageInstaller = ComponentModel.GetService<IVsPackageInstaller>());

        private IVsPackageInstallerServices pkgInstallerServices;

        public IVsPackageInstallerServices PackageInstallerServices
            => pkgInstallerServices ?? (pkgInstallerServices = ComponentModel.GetService<IVsPackageInstallerServices>());

        public IVsMonitorSelection MonitorSelection
        {
            get
            {
                return (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            }
        }

        private SVsServiceProvider svcProvider;

        public SVsServiceProvider ServiceProvider
            => svcProvider ?? (svcProvider = ComponentModel.GetService<SVsServiceProvider>());

        private SolutionEventsListener solutionEventsListener;


        public int MajorVisualStudioVersion => int.Parse(dte.Version.Substring(0, 2));

        private DocumentEvents documentEvents;
        private ProjectItemsEvents projectItemsEvents;

        private DTE dte;

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

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                var cSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidCSharpAddServiceStackReference);
                var cSharpProjectAddReferenceMenuCommand = new OleMenuCommand(CSharpAddReferenceCallback ,cSharpProjContextAddReferenceCommandId);
                cSharpProjectAddReferenceMenuCommand.BeforeQueryStatus += CSharpQueryAddMenuItem;
                mcs.AddCommand(cSharpProjectAddReferenceMenuCommand);

                var fSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpAddServiceStackReference);
                var fSharpProjectContextOleMenuCommand = new OleMenuCommand(FSharpAddReferenceCallback, fSharpProjContextAddReferenceCommandId);
                fSharpProjectContextOleMenuCommand.BeforeQueryStatus += FSharpQueryAddMenuItem;
                mcs.AddCommand(fSharpProjectContextOleMenuCommand);

                var vbNetProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidVbNetAddServiceStackReference);
                var vbNetProjectContextOleMenuCommand = new OleMenuCommand(VbNetAddReferenceCallback, vbNetProjContextAddReferenceCommandId);
                vbNetProjectContextOleMenuCommand.BeforeQueryStatus += VbNetQueryAddMenuItem;
                mcs.AddCommand(vbNetProjectContextOleMenuCommand);

                var typeScriptProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidTypeScriptAddServiceStackReference);
                var typeScriptProjectContextOleMenuCommand = new OleMenuCommand(TypeScriptAddReferenceCallback, typeScriptProjContextAddReferenceCommandId);
                typeScriptProjectContextOleMenuCommand.BeforeQueryStatus += TypeScriptQueryAddMenuItem;
                mcs.AddCommand(typeScriptProjectContextOleMenuCommand);

                var updateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidUpdateServiceStackReference);
                var updateReferenceMenuCommand = new OleMenuCommand(UpdateReferenceCallback,
                    updateReferenceCommandId);
                updateReferenceMenuCommand.BeforeQueryStatus += QueryUpdateMenuItem;
                mcs.AddCommand(updateReferenceMenuCommand);
            }

            solutionEventsListener = new SolutionEventsListener();
            solutionEventsListener.OnAfterOpenSolution += SolutionLoaded;

            RegisterStartupEvents();
        }

        DTEEvents dte_events;

        private void RegisterStartupEvents()
        {
            if(dte == null)
                dte = (DTE)GetService(typeof(DTE));
            

            if (dte != null)
            {
                dte_events = dte.Events.DTEEvents;
                dte_events.OnStartupComplete += OnStartupComplete;
            }
        }

        private void OnStartupComplete()
        {
            dte_events.OnStartupComplete -= OnStartupComplete;
            dte_events = null;
            var cleanupList = TemplateCleanupByVsVersion[MajorVisualStudioVersion];
            foreach (var deleteTemplate in cleanupList)
            {
                DirectoryInfo localVsDir = new DirectoryInfo(Path.Combine(UserLocalDataPath,"Extensions"));
                var ssvsDllPath = localVsDir.GetFiles("ServiceStackVS.dll", SearchOption.AllDirectories);

                // When updating from VSIX file, two dlls might exist.
                foreach (var fileInfo in ssvsDllPath)
                {
                    DirectoryInfo ssvsExtensionDirInfo = fileInfo.Directory;

                    if (ssvsExtensionDirInfo == null)
                    {
                        continue;
                    }
                    var files = ssvsExtensionDirInfo.GetFiles(deleteTemplate + ".zip", SearchOption.AllDirectories);
                    if (files.Length > 0)
                    {
                        try
                        {
                            File.Delete(files[0].FullName);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            //Possible race condition on upgrade, ignore.
                        }
                        catch (IOException)
                        {
                            //Possible race condition on upgrade, ignore.
                        }
                        catch (Exception e)
                        {
                            OutputWindowWriter.WriterWindow.WriteLine("ServiceStackVS had trouble starting. \n\n" + e.Message + "\n" + e.StackTrace);
                        }
                    }
                }
            }
            ServiceProvider.GetWritableSettingsStore().SetPackageReady(true);
        }

        private void SolutionLoaded()
        {
            dte = (DTE)GetService(typeof(DTE));
            if (dte == null)
            {
                Debug.WriteLine("Unable to get the EnvDTE.DTE service.");
                return;
            }

            var events = dte.Events as Events2;
            if (events == null)
            {
                Debug.WriteLine("Unable to get the Events2.");
                return;
            }

            documentEvents = events.get_DocumentEvents();
            projectItemsEvents = events.ProjectItemsEvents;
            projectItemsEvents.ItemAdded += ProjectItemsEventsOnItemAdded;
            documentEvents.DocumentSaved += DocumentEventsOnDocumentSaved;
        }

        private void ProjectItemsEventsOnItemAdded(ProjectItem projectItem)
        {
            
        }

        private void CSharpQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
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
                (string.Equals(project.Kind, VsHelperGuids.CSharpProjectKind,
                    StringComparison.InvariantCultureIgnoreCase) ||
                //Project is Csharp (dotnetcore)
                string.Equals(project.Kind, VsHelperGuids.CSharpDotNetCore,
                    StringComparison.InvariantCultureIgnoreCase));
                //Not busy building

            bool enabled = ready && visible;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        private void FSharpQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
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

        private bool IsProjectReady()
        {
            var monitorSelection = MonitorSelection;
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            return result == VSConstants.S_OK && pfActive > 0;
        }

        private void QueryUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;

            var ready = IsProjectReady();
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            var selectedItems = selectedFiles as IList<SelectedItem> ?? selectedFiles.ToList();

            bool visible = projectItem.ContainingProject != null &&
                           projectItem.ContainingProject.Kind != null &&
                           //Project is not unloaded
                           !string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.ProjectUnloaded,
                               StringComparison.InvariantCultureIgnoreCase);

            if (!visible)
            {
                command.Visible = false;
                command.Enabled = false;
                return;
            }

            var typeHandlers = selectedItems.GetTypeHandlerForSelectedFiles();
            if (typeHandlers.Count == 0)
            {
                command.Visible = false;
                command.Enabled = false;
                return;
            }

            var typeHandler = typeHandlers.First();
            bool validVsProjectType =
                typeHandler.ValidVsProjectTypeGuids().FirstOrDefault(
                    projectTypeGuid =>
                        string.Equals(projectItem.ContainingProject.Kind, projectTypeGuid,
                            StringComparison.InvariantCultureIgnoreCase)) != null;


            // Ensure update button is visable and project is 'ready' (of right type and not building) or item clicked is typescript dtos file.
            bool enabled = ready || (typeHandler.GetType() == typeof(TypeScriptNativeTypesHandler) || 
                typeHandler.GetType() == typeof(TypeScriptConcreteNativeTypesHandler));

            command.Visible = validVsProjectType;
            command.Enabled = enabled;
        }
        
        private void VbNetQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;
            var project = VSIXUtils.GetSelectedProject();

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

        private void TypeScriptQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            // Adding static files while running supported by VS 2015+
            if (MajorVisualStudioVersion > 11)
            {
                guid = VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_guid;
            }
            uint contextCookie;
            int pfActive;
            monitorSelection.GetCmdUIContextCookie(ref guid, out contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;

            var isProjectItemAFolder = VSIXUtils.IsSelectProjectItemAFolder();

            var isAProjectAndLoaded = VSIXUtils.IsSelectedItemAReadyProject();

            bool visible = isAProjectAndLoaded || isProjectItemAFolder;

            bool enabled = visible && ready;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        #endregion

        private void UpdateReferenceCallback(object sender, EventArgs e)
        {
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            var selectedItems = selectedFiles as IList<SelectedItem> ?? selectedFiles.ToList();
            var typeHandlers = selectedItems.GetTypeHandlerForSelectedFiles();
            if (typeHandlers.Count == 0)
            {
                return;
            }
            var typesHandler = typeHandlers.First();
            UpdateGeneratedDtos(projectItem, typesHandler);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void CSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.CSharpNativeTypesHandler;
            AddServiceStackReference(projectPath, typesHandler);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void FSharpAddReferenceCallback(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.FSharpNativeTypesHandler;
            AddServiceStackReference(projectPath, typesHandler);
        }

        private void VbNetAddReferenceCallback(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.VbNetNativeTypesHandler;
            AddServiceStackReference(projectPath, typesHandler);
        }

        private void TypeScriptAddReferenceCallback(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var folderPath = VSIXUtils.GetSelectedFolderPath();
            string finalPath = projectPath ?? folderPath;
            var typesHandler = NativeTypeHandlers.TypeScriptNativeTypesHandler;
            AddServiceStackReference(finalPath, typesHandler);
        }

        private void AddServiceStackReference(string folderPath, INativeTypesHandler typesHandler)
        {
            int fileNameNumber = 1;
            //Find a version of the default name that doesn't already exist, 
            //mimicing VS default file name behaviour.
            while (File.Exists(Path.Combine(folderPath, "ServiceReference" + fileNameNumber + typesHandler.CodeFileExtension)))
            {
                fileNameNumber++;
            }
            var dialog = new AddServiceStackReference("ServiceReference" + fileNameNumber, typesHandler);
            dialog.ShowDialog();
            if (!dialog.AddReferenceSucceeded)
            {
                return;
            }
            // Change native types handler for TypeScript switching concrete.
            typesHandler = dialog.GetLastNativeTypesHandler();
            string templateCode = dialog.CodeTemplate;
            AddNewDtoFileToProject(dialog.FileNameTextBox.Text + typesHandler.CodeFileExtension, templateCode, typesHandler.RequiredNuGetPackages);
        }

        private void UpdateGeneratedDtos(ProjectItem projectItem, INativeTypesHandler typesHandler)
        {
            OutputWindowWriter.WriterWindow.Show();
            OutputWindowWriter.WriterWindow.ShowOutputPane(dte);
            OutputWindowWriter.WriterWindow.WriteLine("--- Updating ServiceStack Reference '" + projectItem.Name + "' ---");
            string projectItemPath = projectItem.GetFullPath();
            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>().ToList();
            bool isDtoSelected = false;
            isDtoSelected = selectedFiles
                .Any(item => typesHandler.IsHandledFileType(item.Name));

            //Handle FSharp file extension name change for DTO files, eg .dto.fs to .dtos.fs
            if (!isDtoSelected && typesHandler is FSharpNativeTypesHandler)
            {
                isDtoSelected = selectedFiles
                .Any(item => item.Name.ToLowerInvariant()
                    .EndsWith(".dto.fs"));
            }

            if (isDtoSelected)
            {
                string filePath = projectItemPath;
                var existingGeneratedCode = File.ReadAllLines(filePath).Join(Environment.NewLine);

                if (!typesHandler.TryExtractBaseUrl(existingGeneratedCode, out var baseUrl))
                {
                    OutputWindowWriter.WriterWindow.WriteLine("Unable to read URL from DTO file. Please ensure the file was generated correctly from a ServiceStack server.");
                    return;
                }
                try
                {
                    var options = typesHandler.ParseComments(existingGeneratedCode);
                    bool setSslValidationCallback = false;
                    //Don't set validation callback if one has already been set for VS.
                    if (ServicePointManager.ServerCertificateValidationCallback == null)
                    {
                        //Temp set validation callback to return true to avoid common dev server ssl certificate validation issues.
                        setSslValidationCallback = true;
                        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                    }

                    string updatedCode = typesHandler.GetUpdatedCode(baseUrl, options);
                    if (setSslValidationCallback)
                    {
                        //If callback was set to return true, reset back to null.
                        ServicePointManager.ServerCertificateValidationCallback = null;
                    }

                    try
                    {
                        File.WriteAllText(filePath, updatedCode);
                        bool optOutOfStats = dte.GetOptOutStatsSetting();
                        if (!optOutOfStats)
                        {
                            var langName = typesHandler.RelativeTypesUrl.Substring(6);
                            Analytics.SubmitAnonymousUpdateReferenceUsage(langName);
                        }
                    }
                    catch (Exception ex)
                    {
                        OutputWindowWriter.WriterWindow.WriteLine("ServiceStack Reference: File.WriteAllText() - " + ex.Message);
                    }

                }
                catch (Exception e)
                {
                    OutputWindowWriter.WriterWindow.WriteLine("Failed to update ServiceStack Reference: Unhandled error - " + e.Message);
                }

                OutputWindowWriter.WriterWindow.WriteLine("--- Update ServiceStack Reference Complete ---");
            }
            else
            {
                OutputWindowWriter.WriterWindow.WriteLine("--- Valid file not found ServiceStack Reference '" + projectItem.Name + "' ---");
            }
        }

        private void AddNewDtoFileToProject(string fileName, string templateCode, List<string> nugetPackages = null)
        {
            nugetPackages = nugetPackages ?? new List<string>();
            
            var project = VSIXUtils.GetSelectedProject() ?? VSIXUtils.GetSelectObject<ProjectItem>().ContainingProject;

            // This is a temporary work around for ServiceStack *.Core packages whilst they are separate/in beta. 
            // This is to be removed/updated once the Text, Client and/or Interfaces NuGet packages support NetCore by default.
            nugetPackages = ApplyNetCoreNuGetPackages(nugetPackages, project);

            var path = VSIXUtils.GetSelectedProjectPath() ?? VSIXUtils.GetSelectedFolderPath();
            string fullPath = Path.Combine(path, fileName);
            File.WriteAllText(fullPath, templateCode);
            
            // HACK avoid VS2015 Update 2 seems to detect file in use semi regularly.
            Thread.Sleep(20);
            var newDtoFile = project.ProjectItems.AddFromFile(fullPath);
            newDtoFile.Open(EnvDteConstants.vsViewKindCode);
            newDtoFile.Save();
            foreach (var nugetPackage in nugetPackages)
            {
                AddNuGetDependencyIfMissing(project, nugetPackage);
            }
            
            project.Save();
        }

        private List<string> ApplyNetCoreNuGetPackages(List<string> nugetPackages, EnvDTE.Project project)
        {
            List<string> result = new List<string>();
            if (//Project is Csharp (dotnetcore)
                string.Equals(project.Kind, VsHelperGuids.CSharpDotNetCore,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                nugetPackages.ForEach(x => result.Add(x + ".Core"));
            }
            else
            {
                return nugetPackages;
            }

            return result;
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
            document.HandleDocumentSaved(OutputWindowWriter.WriterWindow);
        }
    }
}

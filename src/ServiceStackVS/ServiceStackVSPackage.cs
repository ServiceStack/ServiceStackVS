using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using NuGet.VisualStudio;
using ServiceStack;
using ServiceStackVS.Common;
using ServiceStackVS.NativeTypes;
using ServiceStackVS.NativeTypes.Handlers;
using ServiceStackVS.NativeTypesWizard;
using ServiceStackVS.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
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
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", Analytics.VERSION, IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVSServiceStackPkgString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(ServiceStackOptionsDialogGrid), "ServiceStack", "General", 0, 0, true)]
    public sealed class ServiceStackVSPackage : AsyncPackage
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

        public async Task<IComponentModel> GetComponentModelAsync() => 
            (IComponentModel)await GetServiceAsync(typeof(SComponentModel));

        public IVsMonitorSelection MonitorSelection => (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));

        private SVsServiceProvider svcProvider;
        public async Task<SVsServiceProvider> GetServiceProviderAsync() => 
            svcProvider ?? (svcProvider = (await GetComponentModelAsync()).GetService<SVsServiceProvider>());

        private SolutionEventsListener solutionEventsListener;


        public int MajorVisualStudioVersion => int.Parse(dte.Version.Substring(0, 2));

        private DocumentEvents documentEvents;
        private ProjectItemsEvents projectItemsEvents;

        private DTE dte;

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members
        // https://github.com/Microsoft/VSSDK-Extensibility-Samples/tree/master/AsyncPackageMigration
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);

            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            var packageInstaller = (await GetComponentModelAsync()).GetService<IVsPackageInstaller>();
            var pkgInstallerServices = (await GetComponentModelAsync()).GetService<IVsPackageInstallerServices>();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = await GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            if (mcs != null)
            {
                // Create the command for the menu item.
                var cSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidCSharpAddServiceStackReference);
                var cSharpProjectAddReferenceMenuCommand = new OleMenuCommand(async (s,e) => await CSharpAddReferenceCallbackAsync(s,e), cSharpProjContextAddReferenceCommandId);
                cSharpProjectAddReferenceMenuCommand.BeforeQueryStatus += CSharpQueryAddMenuItem;
                mcs.AddCommand(cSharpProjectAddReferenceMenuCommand);

                var fSharpProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidFSharpAddServiceStackReference);
                var fSharpProjectContextOleMenuCommand = new OleMenuCommand(async (s,e) => await FSharpAddReferenceCallbackAsync(s,e), fSharpProjContextAddReferenceCommandId);
                fSharpProjectContextOleMenuCommand.BeforeQueryStatus += FSharpQueryAddMenuItem;
                mcs.AddCommand(fSharpProjectContextOleMenuCommand);

                var vbNetProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidVbNetAddServiceStackReference);
                var vbNetProjectContextOleMenuCommand = new OleMenuCommand(async (s,e) => await VbNetAddReferenceCallbackAsync(s,e), vbNetProjContextAddReferenceCommandId);
                vbNetProjectContextOleMenuCommand.BeforeQueryStatus += VbNetQueryAddMenuItem;
                mcs.AddCommand(vbNetProjectContextOleMenuCommand);

                var typeScriptProjContextAddReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidTypeScriptAddServiceStackReference);
                var typeScriptProjectContextOleMenuCommand = new OleMenuCommand(async (s,e) => await TypeScriptAddReferenceCallbackAsync(s,e), typeScriptProjContextAddReferenceCommandId);
                typeScriptProjectContextOleMenuCommand.BeforeQueryStatus += TypeScriptQueryAddMenuItem;
                mcs.AddCommand(typeScriptProjectContextOleMenuCommand);

                var updateReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidUpdateServiceStackReference);
                var updateReferenceMenuCommand = new OleMenuCommand(async (s, e) => await UpdateReferenceCallbackAsync(s,e), updateReferenceCommandId);
                updateReferenceMenuCommand.BeforeQueryStatus += QueryUpdateMenuItem;
                mcs.AddCommand(updateReferenceMenuCommand);
            }

            solutionEventsListener = new SolutionEventsListener();
            solutionEventsListener.OnAfterOpenSolution += SolutionLoaded;
        }

        EnvDTE.DTEEvents dte_events;

        private void OnStartupComplete()
        {
            dte_events.OnStartupComplete -= OnStartupComplete;
            dte_events = null;
            var cleanupList = TemplateCleanupByVsVersion[MajorVisualStudioVersion];
            var localVsDir = new DirectoryInfo(Path.Combine(UserLocalDataPath,"Extensions"));
            foreach (var deleteTemplate in cleanupList)
            {
                var ssvsDllPath = localVsDir.GetFiles("ServiceStackVS.dll", SearchOption.AllDirectories);

                // When updating from VSIX file, two dlls might exist.
                foreach (var fileInfo in ssvsDllPath)
                {
                    var ssvsExtensionDirInfo = fileInfo.Directory;

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
                            HandleException(e, "ServiceStackVS had trouble starting.");
                        }
                    }
                }
            }

            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                (await GetComponentModelAsync()).GetService<SVsServiceProvider>()
                    .GetWritableSettingsStore().SetPackageReady(true);
            });
        }

        private void SolutionLoaded()
        {
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                dte = (DTE)(await this.GetServiceAsync(typeof(DTE)));
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
            });
        }

        private void ProjectItemsEventsOnItemAdded(ProjectItem projectItem)
        {
            
        }

        private void CSharpQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            monitorSelection.GetCmdUIContextCookie(ref guid, out var contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out var pfActive);
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
            monitorSelection.GetCmdUIContextCookie(ref guid, out var contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out var pfActive);
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
            monitorSelection.GetCmdUIContextCookie(ref guid, out var contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out var pfActive);
            return result == VSConstants.S_OK && pfActive > 0;
        }

        private void QueryUpdateMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;

            var ready = IsProjectReady();
            var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();

            var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
            var selectedItems = selectedFiles as IList<SelectedItem> ?? selectedFiles.ToList();

            bool visible = projectItem.ContainingProject?.Kind != null && 
                !string.Equals(projectItem.ContainingProject.Kind, VsHelperGuids.ProjectUnloaded, StringComparison.InvariantCultureIgnoreCase);

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


            // Ensure update button is visible and project is 'ready' (of right type and not building) or item clicked is typescript DTOs file.
            bool enabled = ready || (typeHandler.GetType() == typeof(TypeScriptNativeTypesHandler) || 
                typeHandler.GetType() == typeof(TypeScriptConcreteNativeTypesHandler) || typeHandler.GetType() == typeof(EsModuleNativeTypesHandler));

            command.Visible = validVsProjectType;
            command.Enabled = enabled;
        }
        
        private void VbNetQueryAddMenuItem(object sender, EventArgs eventArgs)
        {
            var command = (OleMenuCommand)sender;
            var monitorSelection = (IVsMonitorSelection)GetService(typeof(IVsMonitorSelection));
            var guid = VSConstants.UICONTEXT.SolutionExistsAndNotBuildingAndNotDebugging_guid;
            monitorSelection.GetCmdUIContextCookie(ref guid, out var contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out var pfActive);
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

            monitorSelection.GetCmdUIContextCookie(ref guid, out var contextCookie);
            var result = monitorSelection.IsCmdUIContextActive(contextCookie, out var pfActive);
            var ready = result == VSConstants.S_OK && pfActive > 0;

            var isProjectItemAFolder = VSIXUtils.IsSelectProjectItemAFolder();

            var isAProjectAndLoaded = VSIXUtils.IsSelectedItemAReadyProject();

            bool visible = isAProjectAndLoaded || isProjectItemAFolder;

            bool enabled = visible && ready;

            command.Visible = visible;
            command.Enabled = enabled;
        }

        #endregion

        private void HandleException(Exception ex, string prefix=null, bool ignoreOutputWindow=false)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
                sb.AppendLine($"[{prefix}]");

            sb.AppendLine(ex.Message + ":");
            sb.AppendLine(ex.ToString());

            try
            {
                if (!ignoreOutputWindow)
                {
                    OutputWindowWriter.WriterWindow.Show();
                    OutputWindowWriter.WriterWindow.ShowOutputPane(dte);
                    OutputWindowWriter.WriterWindow.WriteLine(sb.ToString());
                }
            }
            catch (Exception showEx)
            {
                sb.AppendLine("OutputWindowWriter.WriterWindow.Show(): " + showEx.Message);
                sb.AppendLine(showEx.ToString());
            }

            try
            {
                ActivityLog.TryLogError("ServiceStackVS", sb.ToString());
            }
            catch (Exception activityLogEx)
            {
                sb.AppendLine("ActivityLog.TryLogError(): " + activityLogEx.Message);
                sb.AppendLine(activityLogEx.ToString());
            }

            var debugLogPath = "C:\\src\\ServiceStackVS.debug.log";
            if (File.Exists(debugLogPath))
            {
                File.AppendAllText(debugLogPath, sb.ToString());
            }
        }

        private void LogOutputWindow(string message, bool showPane=false)
        {
            try
            {
                OutputWindowWriter.WriterWindow.Show();
                OutputWindowWriter.WriterWindow.ShowOutputPane(dte);
                OutputWindowWriter.WriterWindow.WriteLine(message);
                ActivityLog.TryLogInformation("ServiceStackVS", message);
            }
            catch (Exception e)
            {
                HandleException(e, nameof(LogOutputWindow), ignoreOutputWindow:true);
            }
        }

        private async Task UpdateReferenceCallbackAsync(object sender, EventArgs e)
        {
            try
            {
                var projectItem = VSIXUtils.GetSelectObject<ProjectItem>();
                var selectedFiles = projectItem.DTE.SelectedItems.Cast<SelectedItem>();
                var selectedItems = selectedFiles as IList<SelectedItem> ?? selectedFiles.ToList();
                var typeHandlers = selectedItems.GetTypeHandlerForSelectedFiles();
                if (typeHandlers.Count == 0)
                    return;

                var typesHandler = typeHandlers.First();
                await UpdateGeneratedDtosAsync(projectItem, typesHandler);
            }
            catch (Exception ex)
            {
                HandleException(ex, nameof(UpdateReferenceCallbackAsync));
            }
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private async Task CSharpAddReferenceCallbackAsync(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.CSharpNativeTypesHandler;
            await AddServiceStackReferenceAsync(projectPath, typesHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private async Task FSharpAddReferenceCallbackAsync(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.FSharpNativeTypesHandler;
            await AddServiceStackReferenceAsync(projectPath, typesHandler).ConfigureAwait(false);
        }

        private async Task VbNetAddReferenceCallbackAsync(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var typesHandler = NativeTypeHandlers.VbNetNativeTypesHandler;
            await AddServiceStackReferenceAsync(projectPath, typesHandler).ConfigureAwait(false);
        }

        private async Task TypeScriptAddReferenceCallbackAsync(object sender, EventArgs e)
        {
            var projectPath = VSIXUtils.GetSelectedProjectPath();
            var folderPath = VSIXUtils.GetSelectedFolderPath();
            string finalPath = projectPath ?? folderPath;
            var typesHandler = NativeTypeHandlers.TypeScriptConcreteNativeTypesHandler;
            await AddServiceStackReferenceAsync(finalPath, typesHandler).ConfigureAwait(false);
        }

        private async Task AddServiceStackReferenceAsync(string folderPath, INativeTypesHandler typesHandler)
        {
            try
            {
                
                //Find a version of the default name that doesn't already exist, 
                //mimicking VS default file name behaviour.

                var baseFileName = "dtos";

                string FindInitialFilename(string fname)
                {
                    if (fname == baseFileName && !File.Exists(Path.Combine(folderPath,typesHandler.CodeFileExtension.Substring(1)))) {
                        return fname;
                    }
                    int fileNameNumber = 1;
                    
                    while(File.Exists(Path.Combine(folderPath,
                    fname + fileNameNumber + typesHandler.CodeFileExtension))) {
                        fileNameNumber++;
                    }
                    return fname + fileNameNumber;
                }

                var initFileName = FindInitialFilename(baseFileName);
                var dialog = new AddServiceStackReference(initFileName, typesHandler);
                dialog.ShowDialog();
                if (!dialog.AddReferenceSucceeded)
                {
                    return;
                }
                // Change native types handler for TypeScript switching concrete.
                typesHandler = dialog.GetLastNativeTypesHandler();

                var fileName = dialog.FileNameTextBox.Text + typesHandler.CodeFileExtension;
                // Handle "dtos" default filename.
                if (dialog.FileNameTextBox.Text == baseFileName)
                    fileName = typesHandler.CodeFileExtension.Substring(1);
                // Handle user providing full filename (must still include dtos.{ext}).
                if (dialog.FileNameTextBox.Text.EndsWithIgnoreCase(typesHandler.CodeFileExtension))
                    fileName = dialog.FileNameTextBox.Text;

                var templateCode = dialog.CodeTemplate;
                var result = await AddNewDtoFileToProject(fileName, templateCode, typesHandler.RequiredNuGetPackages);
                if (result && dialog.AddReferenceSucceeded == true)
                {
                    await SubmitAddRefStatsAsync(typesHandler);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Failed to save DTO");
            }
            return;
        }

        private async Task SubmitAddRefStatsAsync(INativeTypesHandler typesHandler)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            dte = (DTE)(await this.GetServiceAsync(typeof(DTE)));
            if (dte != null)
            {
                bool optOutOfStats = dte.GetOptOutStatsSetting();
                if (!optOutOfStats)
                {
                    var langName = typesHandler.RelativeTypesUrl.Substring(6);
                    await Analytics.SubmitAnonymousAddReferenceUsage(langName);
                }
            }
            else
            {
                OutputWindowWriter.WriterWindow.WriteLine("Warning: Failed to resolve DTE");
            }
        }

        private async Task UpdateGeneratedDtosAsync(ProjectItem projectItem, INativeTypesHandler typesHandler)
        {
            LogOutputWindow($"--- Updating ServiceStack Reference '{projectItem.Name}' ---", showPane:true);
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
                    LogOutputWindow("Unable to read URL from DTO file. Please ensure the file was generated correctly from a ServiceStack server.");
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
                            await Analytics.SubmitAnonymousAddReferenceUsage(langName);
                        }
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex, "ServiceStack Reference: File.WriteAllText()");
                    }

                }
                catch (Exception e)
                {
                    HandleException(e, "Failed to update ServiceStack Reference: Unhandled error");
                }

                LogOutputWindow("--- Update ServiceStack Reference Complete ---");
            }
            else
            {
                LogOutputWindow("--- Valid file not found ServiceStack Reference '" + projectItem.Name + "' ---");
            }
        }

        private async Task<bool> AddNewDtoFileToProject(string fileName, string templateCode, List<string> nugetPackages = null)
        {
            nugetPackages = nugetPackages ?? new List<string>();
            
            var project = VSIXUtils.GetSelectedProject() ?? VSIXUtils.GetSelectObject<ProjectItem>().ContainingProject;

            var path = VSIXUtils.GetSelectedProjectPath() ?? VSIXUtils.GetSelectedFolderPath();
            string fullPath = Path.Combine(path, fileName);
            File.WriteAllText(fullPath, templateCode);

            bool success = true;
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                // HACK avoid VS2015 Update 2 seems to detect file in use semi regularly.
                await Task.Delay(50);
                var newDtoFile = project.ProjectItems.AddFromFile(fullPath);
                project.Save();
                newDtoFile.Open(EnvDTE.Constants.vsViewKindCode);
                newDtoFile.Save();
                newDtoFile.Document.Activate();

            }
            catch (Exception) {
                success = false;
            }

            try
            {
                foreach (var nugetPackage in nugetPackages)
                {
                    AddNuGetDependencyIfMissing(project, nugetPackage);
                }
            }
            catch (Exception) {
                success = false;
            }

            try
            {
                project.Save();
            }
            catch (Exception) {
                success = false;
            }

            return success;
        }

        private void AddNuGetDependencyIfMissing(Project project,string packageId)
        {
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                //Once the generated code has been added, we need to ensure that  
                //the required ServiceStack.Interfaces package is installed.
                var installedPackages = (await GetComponentModelAsync()).GetService<IVsPackageInstallerServices>()
                    .GetInstalledPackages(project);

                //TODO check project references in-case ServiceStack.Interfaces is referenced via local file.
                //VS has different ways to check different types of projects for refs, need to find method to check all.

                //Check if existing nuget reference exists
                if (installedPackages.FirstOrDefault(x => x.Id == packageId) == null)
                {
                    var service = (await GetComponentModelAsync()).GetService<IVsPackageInstaller>();
                    (await GetComponentModelAsync()).GetService<IVsPackageInstaller>()
                        .InstallPackage(null,
                        project,
                        packageId,
                        version: (string)null, //Latest version of packageId
                        ignoreDependencies: false);
                }
            });
        }

        private void DocumentEventsOnDocumentSaved(Document document)
        {
            document.HandleDocumentSaved(OutputWindowWriter.WriterWindow);
        }
    }
}

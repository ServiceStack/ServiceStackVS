using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;
using ServiceStack;
using ServiceStack.Text;
using VSServiceStack.Types;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using MessageBox = System.Windows.MessageBox;
using Thread = System.Threading.Thread;

namespace VSServiceStack
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
    public sealed class VSServiceStackPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VSServiceStackPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



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
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID projContextServiceStackReferenceCommandId = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidServiceStackReference);
                var projContextServiceStackReferenceCommand = new OleMenuCommand(MenuItemCallback ,projContextServiceStackReferenceCommandId);
                projContextServiceStackReferenceCommand.BeforeQueryStatus += BeforeQueryStatusForProjectAddMenuItem;
                mcs.AddCommand(projContextServiceStackReferenceCommand);
            }
        }

        private void BeforeQueryStatusForProjectAddMenuItem(object sender, EventArgs eventArgs)
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

        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var t4TemplateBase = Resources.ServiceModelTemplate;
            string templateCode = null;
            var project = VSIXUtils.GetSelectedProject();
            string projectPath = project.Properties.Item("FullPath").Value.ToString();
            int fileNameNumber = 1;
            //Find a version of the default name that doesn't already exist, 
            //mimicing VS default file name behaviour.
            while (File.Exists(Path.Combine(projectPath, "ServiceReference" + fileNameNumber + ".tt")))
            {
                fileNameNumber++;
            }
            var dialog = new AddServiceStackReference(url => TryResolveServiceStackTemplate(url, t4TemplateBase, out templateCode), 
                "ServiceReference" + fileNameNumber);
            dialog.ShowDialog();
            if (!dialog.AddReferenceSucceeded)
            {
                return;
            }

            CreateAndAddTemplateToProject(dialog, templateCode);
        }

        private void CreateAndAddTemplateToProject(AddServiceStackReference dialog, string templateCode)
        {
            var fileName = dialog.FileNameTextBox.Text + ".tt";
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
            project.ProjectItems.AddFromFile(fullPath.Replace(".tt", ".cs"));
            project.Save();

            //Once the generated code has been added, we need to ensure that  
            //the required ServiceStack.Interfaces package is installed.
            var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
            var installer = componentModel.GetService<IVsPackageInstaller>();
            var installerServices = componentModel.GetService<IVsPackageInstallerServices>();
            var installedPackages = installerServices.GetInstalledPackages(project);
           
            //TODO check project references incase ServiceStack.Interfaces is referenced via local file.
            //Check if existing nuget reference exists
            if (installedPackages.FirstOrDefault(x => x.Id == "ServiceStack.Interfaces") == null)
            {
                installer.InstallPackage("https://www.nuget.org/api/v2/",
                         project,
                         "ServiceStack.Interfaces",
                         version: (string)null, //Latest version of ServiceStack.Interfaces
                         ignoreDependencies: false); 
                project.Save();
            }
        }

        private static bool TryResolveServiceStackTemplate(string url, string t4TemplateBase, out string templateCode)
        {
            string serverUrl = url;
            //Appends trailing forward slash to url if missing
            if (!serverUrl.EndsWith("/"))
            {
                serverUrl = serverUrl + "/";
            }
            templateCode = t4TemplateBase.Replace("$serviceurl$", serverUrl);
            Uri validatedUri;
            bool isValidUri = Uri.TryCreate(serverUrl, UriKind.Absolute, out validatedUri) &&
                              validatedUri.Scheme == Uri.UriSchemeHttp;
            if (isValidUri)
            {
                var metadata = new WebClient().DownloadString(validatedUri + "types/metadata?format=json");
                MetadataTypes metaDataDto;
                try
                {
                    metaDataDto = JsonSerializer.DeserializeFromString<MetadataTypes>(metadata);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed deserializing metadata from server", ex);
                }
                if (metaDataDto.Operations.Count == 0)
                {
                    throw new Exception("Invalid or empty metadata from server");
                }
                return true;
            }
            return false;
        }

        public static string DownloadCSharpDtos(string baseUrl)
        {
            var sb = new System.Text.StringBuilder();
            var fields = typeof(CodegenOptions).GetFields(
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var config = new CodegenOptions();
            foreach (var f in fields)
            {
                var value = f.GetValue(config);
                if (value == null) continue;
                if (sb.Length > 0) sb.Append("&");
                sb.AppendFormat("{0}={1}", f.Name, value);
            }
            var qs = sb.ToString();
            if (qs.Length > 0) baseUrl += "?" + qs;
            return new WebClient().DownloadString(baseUrl);
        }

    }

    public class CodegenOptions
    {
        bool? MakePartial;
        bool? MakeVirtual;
        bool? MakeDataContractsExtensible;
        bool? InitializeCollections;
        bool? AddReturnMarker;
        bool? AddDescriptionAsComments;
        bool? AddDataContractAttributes;
        bool? AddDataAnnotationAttributes;
        bool? AddIndexesToDataMembers;
        bool? AddResponseStatus;
        int? AddImplicitVersion;
        string AddDefaultXmlNamespace;
    }
}

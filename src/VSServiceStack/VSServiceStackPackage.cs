using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
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
                CommandID menuCommandID = new CommandID(GuidList.guidVSServiceStackCmdSet, (int)PkgCmdIDList.cmdidServiceStackReference);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID );
                mcs.AddCommand( menuItem );
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var dialog = new AddServiceStackReference();
            dialog.ShowDialog();
            if (!dialog.OkPressed)
            {
                return;
            }
            string serverUrl = dialog.UrlTextBox.Text;
            Uri validatedUri;
            bool isValidUri = Uri.TryCreate(serverUrl, UriKind.Absolute, out validatedUri) && validatedUri.Scheme == Uri.UriSchemeHttp;
            if (isValidUri)
            {
                var dtoCode = DownloadCSharpDtos(validatedUri.AbsoluteUri + "types/csharp");
                var metadata = new System.Net.WebClient().DownloadString(validatedUri + "types/metadata?format=json");
                var metaDataDto = JsonSerializer.DeserializeFromString<MetadataTypes>(metadata);
                if (metaDataDto.Operations.Count == 0)
                {
                    throw new Exception("Invalid metadata from server");
                }
                var fileName = metaDataDto.Operations[0].Request.Name + ".cs";
                var project = GetSelectedProject();
                string projectPath = project.Properties.Item("FullPath").Value.ToString();
                string fullPath = Path.Combine(projectPath, fileName);
                using (var streamWriter = File.CreateText(fullPath))
                {
                    streamWriter.Write(dtoCode);
                    streamWriter.Flush();
                }
                project.ProjectItems.AddFromFile(fullPath);
            }
            else
            {
                MessageBox.Show("Invalid url provided, please provide a valid http url");
            }
            
            //Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
            //           0,
            //           ref clsid,
            //           "VSServiceStack",
            //           string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
            //           string.Empty,
            //           0,
            //           OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //           OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
            //           OLEMSGICON.OLEMSGICON_INFO,
            //           0,        // false
            //           out result));
        }

        /// <summary>
        /// Gets the selected project.
        /// From http://uisurumadushanka89.blogspot.com.au/2013/04/visual-studio-extensibility-get-active.html
        /// </summary>
        /// <returns></returns>
        public Project GetSelectedProject()
        {
            IntPtr hierarchyPointer, selectionContainerPointer;
            Object selectedObject = null;
            IVsMultiItemSelect multiItemSelect;
            uint projectItemId;

            IVsMonitorSelection monitorSelection =
                    (IVsMonitorSelection)Package.GetGlobalService(
                    typeof(SVsShellMonitorSelection));

            monitorSelection.GetCurrentSelection(out hierarchyPointer,
                                                 out projectItemId,
                                                 out multiItemSelect,
                                                 out selectionContainerPointer);

            IVsHierarchy selectedHierarchy = null;
            try
            {
                selectedHierarchy = Marshal.GetTypedObjectForIUnknown(
                                                     hierarchyPointer,
                                                     typeof(IVsHierarchy)) as IVsHierarchy;
            }
            catch (Exception)
            {
                return null;
            }

            if (selectedHierarchy != null)
            {
                ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(
                                                  projectItemId,
                                                  (int)__VSHPROPID.VSHPROPID_ExtObject,
                                                  out selectedObject));
            }

            Project selectedProject = selectedObject as Project;

            return selectedProject;
        }

        public static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;
            int hr = VSConstants.S_OK;
            var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            if (monitorSelection == null || solution == null)
            {
                return false;
            }
            IVsMultiItemSelect multiItemSelect = null;
            IntPtr hierarchyPtr = IntPtr.Zero;
            IntPtr selectionContainerPtr = IntPtr.Zero;
            try
            {
                hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);
                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemid == VSConstants.VSITEMID_NIL)
                {
                    // there is no selection
                    return false;
                }
                // multiple items are selected
                if (multiItemSelect != null) return false;
                // there is a hierarchy root node selected, thus it is not a single item inside a project
                if (itemid == VSConstants.VSITEMID_ROOT) return false;
                hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
                if (hierarchy == null) return false;
                Guid guidProjectID = Guid.Empty;
                if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectID)))
                {
                    return false; // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid
                }
                // if we got this far then there is a single project item selected
                return true;
            }
            finally
            {
                if (selectionContainerPtr != IntPtr.Zero)
                {
                    Marshal.Release(selectionContainerPtr);
                }
                if (hierarchyPtr != IntPtr.Zero)
                {
                    Marshal.Release(hierarchyPtr);
                }
            }
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
            return new System.Net.WebClient().DownloadString(baseUrl);
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

using Gtk;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using MonoDevelop.Core;
using MonoDevelop.Projects.Formats.MSBuild;
using MonoDevelop.PackageManagement;
using System.Linq;
using ServiceStackVS.NativeTypes.Handlers;
using System.IO;
using System;
using ServiceStackVS.NativeTypes;
using ICSharpCode;
using NuGet;
using ICSharpCode.PackageManagement;

namespace ServiceStackXS.Commands
{
	public class AddReferenceHandler : CommandHandler
	{
		protected override void Run()
		{
			DotNetProject project = IdeApp.ProjectOperations.CurrentSelectedProject as DotNetProject;
			var dotNetProjectProxy = new DotNetProjectProxy (project);
			if (project == null) {
				return;
			}
			INativeTypesHandler nativeTypesHandler = null;
			//TODO Need to work out valid states for SupportedLanguages to better support Add ref usages.
			if (project.SupportedLanguages.Contains ("C#")) {
				nativeTypesHandler = new CSharpNativeTypesHandler ();
			}

			if (project.SupportedLanguages.Contains ("VBNet")) {
				nativeTypesHandler = new VbNetNativeTypesHandler ();
			}

			if (nativeTypesHandler == null) {
				throw new ArgumentNullException ("No supported languages found");
			}

			string fileName = "ServiceReference";
			string finalFileName = "";
			int count = 0;
			bool exists = true;
			while (exists) {
				count++;
				var existingFile = project.Files.FirstOrDefault (x => x.FilePath.FileName == fileName + count.ToString() + nativeTypesHandler.CodeFileExtension);
				exists = existingFile != null;
				finalFileName = fileName + count.ToString () + nativeTypesHandler.CodeFileExtension;
			}
				
			var dialog = new AddReferenceDialog (fileName + count.ToString(),nativeTypesHandler);
			dialog.Run ();

			if (!dialog.AddReferenceSucceeded) {
				return;
			}

			string code = dialog.CodeTemplate;
			dialog.Destroy ();
			string fullPath = Path.Combine (project.BaseDirectory.FullPath.ToString(), finalFileName);
			using (var streamWriter = File.CreateText (fullPath)) {
				streamWriter.Write (code);
				streamWriter.Flush ();
			}
			project.AddFile (fullPath,BuildAction.Compile);

			try {
				AddNuGetPackageReference(dotNetProjectProxy, "ServiceStack.Client");
				AddNuGetPackageReference(dotNetProjectProxy, "ServiceStack.Interfaces");
				AddNuGetPackageReference(dotNetProjectProxy, "ServiceStack.Text");
			} catch (Exception ex) {
				//TODO Error message for user
				var messageDialog = new MessageDialog (
					(Gtk.Window)IdeApp.Workbench.RootWindow.Toplevel,
					DialogFlags.Modal, 
					MessageType.Warning, 
					ButtonsType.Close, 
					"An error occurred trying to add required NuGet packages. Error : " + ex.Message + 
					"\r\n\r\nGenerated service reference will require ServiceStack.Interfaces as a minimum.");
				messageDialog.Run ();
				messageDialog.Destroy ();
			}
		}

		void AddNuGetPackageReference(IDotNetProject project,string packageId)
		{
			var packageRepoFactory = new SharpDevelopPackageRepositoryFactory();
			var packageRepo = packageRepoFactory.CreateRepository ("http://www.nuget.org/api/v2/");
			var packageManagementProjectFactory = new PackageManagementProjectFactory (PackageManagementServices.PackageManagementEvents);
			var packageManagementProject = packageManagementProjectFactory.CreateProject (packageRepo, project);
			var package = packageRepo.FindPackagesById (packageId).FirstOrDefault (x => x.IsLatestVersion);
			var packageManagerFactory = new SharpDevelopPackageManagerFactory ();
			var packageManager = packageManagerFactory.CreatePackageManager (packageRepo, project);
			packageManager.InstallPackage (package, false, false);
			packageManagementProject.AddPackageReference (package);
		}

		protected override void Update (CommandInfo info)
		{

		}
	}
}
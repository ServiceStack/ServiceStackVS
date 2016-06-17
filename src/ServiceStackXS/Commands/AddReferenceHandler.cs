using Gtk;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using MonoDevelop.Core;
using System.Linq;
using ServiceStackVS.NativeTypes.Handlers;
using System.IO;
using System;
using ServiceStackVS.NativeTypes;
using ICSharpCode;
using NuGet;
using System.Threading.Tasks;
using MonoDevelop.PackageManagement;
using System.Collections.Generic;

namespace ServiceStackXS.Commands
{
	public class AddReferenceHandler : CommandHandler
	{
		protected override void Run()
		{
			DotNetProject project = IdeApp.ProjectOperations.CurrentSelectedProject as DotNetProject;
			if (project == null) {
				return;
			}
			INativeTypesHandler nativeTypesHandler = null;
			//SupportedLanguage names currently returns an empty item and the 'languageName' the DotNetProject
			//is initialized with. This might be an array to enable extension for other projects? DotNetProject 
			//only returns an empty and languageName. See source link.
			// https://github.com/mono/monodevelop/blob/dcafac668cbe8f63b4e42ea7f8f032f13aba8221/main/src/core/MonoDevelop.Core/MonoDevelop.Projects/DotNetProject.cs#L198
			if (project.SupportedLanguages.Contains ("C#")) {
				nativeTypesHandler = NativeTypeHandlers.CSharpNativeTypesHandler;
			}

			if (project.SupportedLanguages.Contains ("F#")) {
				nativeTypesHandler = NativeTypeHandlers.FSharpNativeTypesHandler;
			}

			if (project.SupportedLanguages.Contains ("VBNet")) {
				nativeTypesHandler = NativeTypeHandlers.VbNetNativeTypesHandler;
			}

			if (nativeTypesHandler == null) {
				throw new ArgumentNullException ("No supported languages found");
			}

			string fileName = "ServiceReference";

			int count = 0;
			bool exists = true;
			while (exists) {
				count++;
				var existingFile = project.Files.FirstOrDefault (x => x.FilePath.FileName == fileName + count.ToString () + nativeTypesHandler.CodeFileExtension);
				exists = existingFile != null;
			}
				
			var dialog = new AddReferenceDialog (fileName + count.ToString (), nativeTypesHandler);
			dialog.Run ();
			string finalFileName = dialog.ReferenceName + nativeTypesHandler.CodeFileExtension;
			string code = dialog.CodeTemplate;
			dialog.Destroy ();
			if (!dialog.AddReferenceSucceeded) {
				return;
			}
			IdeApp.Workbench.StatusBar.ShowReady ();
			IdeApp.Workbench.StatusBar.ShowMessage ("Adding ServiceStack Reference...");
			IdeApp.Workbench.StatusBar.Pulse ();
			string fullPath = Path.Combine (project.BaseDirectory.FullPath.ToString (), finalFileName);
			using (var streamWriter = File.CreateText (fullPath)) {
				streamWriter.Write (code);
				streamWriter.Flush ();
			}

			project.AddFile (fullPath, BuildAction.Compile);

			if(!SettingsWidget.OptOut)
				Analytics.SubmitAnonymousAddReferenceUsage(nativeTypesHandler.RelativeTypesUrl.Substring(6));

			try {
				Task.Run (() => { 
					AddNuGetPackageReference (project, "ServiceStack.Client");
					AddNuGetPackageReference (project, "ServiceStack.Interfaces");
					AddNuGetPackageReference (project, "ServiceStack.Text");
				}).ContinueWith (task => { 
					IdeApp.Workbench.StatusBar.ShowReady ();
					IdeApp.Workbench.StatusBar.Pulse ();
				},TaskScheduler.FromCurrentSynchronizationContext ());
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
				IdeApp.Workbench.StatusBar.ShowReady ();
				IdeApp.Workbench.StatusBar.Pulse ();
			}
		}

		void AddNuGetPackageReference(DotNetProject project,string packageId)
		{
			var packageRepoFactory = new PackageRepositoryFactory();
			var packageRepo = packageRepoFactory.CreateRepository ("http://www.nuget.org/api/v2/");
			var package = packageRepo.FindPackagesById (packageId).FirstOrDefault (x => x.IsLatestVersion);
			PackageManagementServices.
                 ProjectOperations.
                     InstallPackages("http://www.nuget.org/api/v2/",
									 project,
									 new List<PackageManagementPackageReference>() { new PackageManagementPackageReference(package.Id, package.Version.Version.ToString()) });
		}

		protected override void Update (CommandInfo info)
		{

		}
	}
}
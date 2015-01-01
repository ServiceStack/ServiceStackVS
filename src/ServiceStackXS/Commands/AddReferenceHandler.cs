using Gtk;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using System.Linq;
using ServiceStackVS.NativeTypes.Handlers;
using System;

namespace ServiceStackXS.Commands
{
	public class AddReferenceHandler : CommandHandler
	{
		protected override void Run()
		{
			var project = IdeApp.ProjectOperations.CurrentSelectedProject;
			if (project == null) {
				return;
			}

			string fileName = "ServiceReference";
			string finalFileName = "";
			int count = 0;
			bool exists = true;
			while (exists) {
				count++;
				var existingFile = project.Files.FirstOrDefault (x => x.Name == fileName);
				exists = existingFile != null;
				finalFileName = fileName + count.ToString ();
			}
			AddReferenceDialog dialog = null;

			//TODO Need to work out valid states for SupportedLanguages to better support Add ref usages.
			if (project.SupportedLanguages.Contains ("C#")) {
				dialog = new AddReferenceDialog (finalFileName,new CSharpNativeTypesHandler());
				dialog.ShowNow();
			}

			if (project.SupportedLanguages.Contains ("VBNet")) {
				dialog = new AddReferenceDialog (finalFileName,new VbNetNativeTypesHandler());
				dialog.ShowNow();
			}

			if (dialog == null) {
				throw new ArgumentNullException ("No supported languages found");
			}

			if (dialog.AddReferenceSucceeded) {
				var serviceFile = new MonoDevelop.Projects.ProjectFile (finalFileName);
				serviceFile.Data = dialog.CodeTemplate;
				project.AddFile (serviceFile);
			} else {
				throw new Exception ("Something went wrong but not sure what..");
			}
		}

		protected override void Update (CommandInfo info)
		{
		}
	}
}
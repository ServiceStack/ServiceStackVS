using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using ServiceStackVS.NativeTypes;
using ServiceStack;
using System.IO;
using MonoDevelop.Core;
using Gtk;

namespace ServiceStackXS.Commands
{
    public class UpdateReferenceHandler : CommandHandler
    {
        public UpdateReferenceHandler()
        {
            
        }

        protected override void Run()
        {
			var selectedFile = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
			if(selectedFile == null || selectedFile.Name == null) {
				return;
			}
			var nativeTypeHandler = NativeTypeHandlers
				.All
				.FirstOrDefault(
					handler => handler.IsHandledFileType(selectedFile.Name.ToLowerInvariant()));
			if (nativeTypeHandler == null) {
				return;
			}

			string filePath = selectedFile.Name;
			string existingCode = File.ReadAllLines (filePath).Join (Environment.NewLine);
			string baseUrl;
			if (!nativeTypeHandler.TryExtractBaseUrl (existingCode, out baseUrl)) {
				var messageDialog = new MessageDialog (
					(Gtk.Window)IdeApp.Workbench.RootWindow.Toplevel,
					DialogFlags.Modal, 
					MessageType.Warning, 
					ButtonsType.Close, 
					"Unable to read ServiceStack reference property 'BaseUrl'. Please check it is correct and try again.");
				messageDialog.Run ();
				messageDialog.Destroy ();
				return;
			}

			try {
				var options = nativeTypeHandler.ParseComments(existingCode);
				string updatedCode = nativeTypeHandler.GetUpdatedCode(baseUrl,options);
				using(var streamWriter = File.CreateText(filePath))
				{
					streamWriter.Write(updatedCode);
					streamWriter.Flush();
				}
				//Refresh document
				var openDocument = IdeApp.Workbench.GetDocument(selectedFile.Name);
				FileService.NotifyFileChanged(openDocument.FileName);

				if (!SettingsWidget.OptOut)
					Analytics.SubmitAnonymousUpdateReferenceUsage(nativeTypeHandler.RelativeTypesUrl.Substring(6));

			} catch (Exception ex) {
				var messageDialog = new MessageDialog (
					(Gtk.Window)IdeApp.Workbench.RootWindow.Toplevel,
					DialogFlags.Modal, 
					MessageType.Error, 
					ButtonsType.Close, 
					"Unable to update reference. Error: " + ex.Message);
				messageDialog.Run ();
				messageDialog.Destroy ();
			}
        }
    }
}

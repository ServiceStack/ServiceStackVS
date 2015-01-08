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
					handler => handler.IsHandledFileType(selectedFile.Name.ToLowerInvariant()) != null);
			if (nativeTypeHandler == null) {
				return;
			}

			string filePath = selectedFile.Name;
			string existingCode = File.ReadAllLines (filePath).Join (Environment.NewLine);
			string baseUrl;
			if (!nativeTypeHandler.TryExtractBaseUrl (existingCode, out baseUrl)) {
				//TODO Warning message box for user
			}

			try {
				var options = nativeTypeHandler.ParseComments(existingCode);
				string updatedCode = nativeTypeHandler.GetUpdatedCode(baseUrl,options);
				using(var streamWriter = File.CreateText(filePath))
				{
					streamWriter.Write(updatedCode);
					streamWriter.Flush();
				}

			} catch (Exception ex) {
				//TODO Error message box failed to update
			}
        }


    }
}

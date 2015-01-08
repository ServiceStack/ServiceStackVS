using System;
using Mono.Addins;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System.Collections.Generic;
using System.Linq;
using ServiceStackVS.NativeTypes;

namespace ServiceStackXS
{
	public class ServiceReferenceDtoCondition : ConditionType
	{
		public override bool Evaluate (NodeElement conditionNode)
		{
			var selectedFile = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
			if(selectedFile == null || selectedFile.Name == null) {
				return false;
			}

			bool visible = NativeTypeHandlers.All.FirstOrDefault(handler => handler.IsHandledFileType(selectedFile.Name.ToLowerInvariant())) != null;
			return visible;
		}
	}
}
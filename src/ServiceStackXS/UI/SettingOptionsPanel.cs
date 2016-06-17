using System;
using MonoDevelop.Components;
using MonoDevelop.Ide.Gui.Dialogs;

namespace ServiceStackXS
{
	public class SettingOptionsPanel : OptionsPanel
	{
		SettingsWidget widget;

		public override void ApplyChanges()
		{
			widget.Store();
		}

		public override Control CreatePanelWidget()
		{
			return widget = new SettingsWidget(); 
		}
	}
}


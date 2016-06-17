using System;
using MonoDevelop.Core;

namespace ServiceStackXS
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SettingsWidget : Gtk.Bin
	{
		const string optOutKey = "ServiceStackXS.OptOut";

		public SettingsWidget()
		{
			this.Build();
			this.optOutCheckbox.Active = OptOut;
		}

		public void Store()
		{
			PropertyService.Set(optOutKey, this.optOutCheckbox.Active);
		}

		public static bool OptOut 
		{ 
			get 
			{ 
				return PropertyService.Get<bool>(optOutKey); 
			}
		}
	}
}


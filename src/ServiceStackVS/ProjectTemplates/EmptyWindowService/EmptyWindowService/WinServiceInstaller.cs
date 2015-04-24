using System.ComponentModel;
using System.Configuration.Install;

namespace $safeprojectname$
{
    [RunInstaller(true)]
	public partial class WinServiceInstaller : Installer
	{
		public WinServiceInstaller()
		{
			InitializeComponent();
		}		
	}
}

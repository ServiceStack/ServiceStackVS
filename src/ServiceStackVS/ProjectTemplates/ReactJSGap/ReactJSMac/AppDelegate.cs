using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace $safeprojectname$
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		public static MainWindowController MainWindowController;

		public AppDelegate(){}

		public override void FinishedLaunching (NSObject notification)
		{
#if DEBUG
			//Enable WebInspector in WebView
			var defaults = NSUserDefaults.StandardUserDefaults;
			defaults.SetBool(true, "WebKitDeveloperExtras");
			defaults.Synchronize();
#endif
			MainWindowController = new MainWindowController();
			MainWindowController.Window.MakeKeyAndOrderFront(this);
		}
	}
}


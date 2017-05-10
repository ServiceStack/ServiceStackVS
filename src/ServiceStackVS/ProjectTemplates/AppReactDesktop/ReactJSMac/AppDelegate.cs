using System;
using CoreGraphics;
using Foundation;
using AppKit;
using ObjCRuntime;

namespace $safeprojectname$
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		public static MainWindowController MainWindowController;

		public AppDelegate(){}

		public override void DidFinishLaunching (NSNotification notification)
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


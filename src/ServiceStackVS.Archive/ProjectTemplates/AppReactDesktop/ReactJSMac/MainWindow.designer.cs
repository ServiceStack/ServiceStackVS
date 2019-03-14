// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace $safeprojectname$
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		WebKit.WebView webView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (webView != null) {
				webView.Dispose ();
				webView = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}

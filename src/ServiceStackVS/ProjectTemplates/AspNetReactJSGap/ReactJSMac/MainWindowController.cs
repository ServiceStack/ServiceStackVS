
using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace $safeprojectname$
{
	public partial class MainWindowController : AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController() : base ("MainWindow")
		{
			Initialize();
		}
		
		// Shared initialization code
		void Initialize()
		{
			DisplayAtCenter();
		}

		#endregion

		//Strong Typed Window Property
		public new MainWindow Window 
		{
			get { return (MainWindow)base.Window; }
		}
		
		public void DisplayAtCenter()
		{
			var xPos = Window.Screen.Frame.Width / 2 - Window.Frame.Width / 2;
			var yPos = Window.Screen.Frame.Height / 2 - Window.Frame.Height / 2;
			Window.SetFrame(new CoreGraphics.CGRect(xPos, yPos, Window.Frame.Width, Window.Frame.Height), display:true);
		}

		public void Hide()
		{
			Window.OrderOut(Window);
		}
	}
}


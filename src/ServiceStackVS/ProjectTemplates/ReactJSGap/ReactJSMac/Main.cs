using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace $safeprojectname$
{
	public static class MainClass
	{
		public static string HostUrl = "http://localhost:3337/";

		static void Main (string[] args)
		{
			new AppHost ().Init ().Start ("http://*.3337");

			NSApplication.Init ();
			NSApplication.Main (args);
		}
	}
}


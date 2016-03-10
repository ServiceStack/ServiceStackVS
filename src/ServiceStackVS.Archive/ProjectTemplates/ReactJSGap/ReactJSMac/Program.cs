using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace $safeprojectname$
{
	public static class Program
	{
		public static string HostUrl = "http://localhost:2337/";

		public static AppHost App;
		public static NSMenu MainMenu;

		static void Main (string[] args)
		{
			App = new AppHost();
			App.Init().Start("http://*:2337/");

			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}


using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace $safeprojectname$
{
	public static class MainClass
	{
		public static string HostUrl = "http://127.0.0.1:3337/";

		public static AppHost App;
		public static NSMenu MainMenu;

		static void Main (string[] args)
		{
			App = new AppHost();
			App.Init().Start("http://*:3337/");

			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}


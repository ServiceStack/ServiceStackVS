using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using ServiceStack;
using ServiceStack.Text;
using Squirrel;

namespace $safeprojectname$
{
    static class Program
    {
        public static string HostUrl = "http://localhost:2337/";
        public static AppHost AppHost;
        public static FormMain Form;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
		    AppHost = new AppHost();
            SquirrelAwareApp.HandleEvents(
                OnInitialInstall,
                OnAppUpdate,
                onAppUninstall: OnAppUninstall,
                onFirstRun: OnFirstRun);
		
            Cef.EnableHighDPISupport();
            Cef.Initialize(new CefSettings());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppHost.Init().Start("http://*:2337/");
            "ServiceStack SelfHost listening at {0} ".Fmt(HostUrl).Print();
            Form = new FormMain();
            Form.Disposed += (sender, args) => AppGitHubUpdater.Dispose();
            Application.Run(Form);
        }

        public static void OnInitialInstall(Version version)
        {
            // Hook for first install
            AppGitHubUpdater.CreateShortcutForThisExe();
        }

        public static void OnAppUpdate(Version version)
        {
            // Hook for application update, CheckForUpdates() initiates this.
            AppGitHubUpdater.CreateShortcutForThisExe();
        }

        public static void OnAppUninstall(Version version)
        {
            // Hook for application uninstall
            AppGitHubUpdater.RemoveShortcutForThisExe();
        }

        public static void OnFirstRun()
        {
            // Hook for first run
        }
    }
}

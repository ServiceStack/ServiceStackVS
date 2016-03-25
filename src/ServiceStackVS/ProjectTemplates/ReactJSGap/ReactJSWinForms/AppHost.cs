using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Funq;
using ServiceStack;
using Squirrel;
using $saferootprojectname$.Resources;
using $saferootprojectname$.ServiceInterface;

namespace $safeprojectname$
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly) {}

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //Plugins.Add(new CorsFeature());

            SetConfig(new HostConfig {
                DebugMode = true,
                EmbeddedResourceBaseTypes = { typeof(AppHost), typeof(SharedEmbeddedResources) },
            });
			
			Task.Run(() => CheckForUpdates());
        }
		
		private async void CheckForUpdates()
        {
            using (var mgr = new UpdateManager(AppSettings.GetString("PackageDeployUrl") ?? "..\\..\\Releases".MapProjectPath()))
            {
                var updateInfo = await mgr.CheckForUpdate();
                if (updateInfo.ReleasesToApply.Count > 0)
                {
                    await mgr.DownloadReleases(updateInfo.ReleasesToApply);
                    await mgr.ApplyReleases(updateInfo);
                }
            }
        }

        public void OnInitialInstall(Version version)
        {
            // Hook for first install
        }

        public void OnAppUpdate(Version version)
        {
            // Hook for application update, CheckForUpdates() initiates this.
        }

        public void OnAppUninstall(Version version)
        {
            // Hook for application uninstall
        }

        public void OnFirstRun()
        {
            // Hook for first run
        }
    }
}

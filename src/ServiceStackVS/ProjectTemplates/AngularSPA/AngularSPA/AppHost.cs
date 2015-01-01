using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using Funq;
using $safeprojectname$.ServiceInterface;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Razor;

namespace $safeprojectname$
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly)
        {
            var customSettings = new FileInfo(@"~/appsettings.txt".MapHostAbsolutePath());
            AppSettings = customSettings.Exists
                ? (IAppSettings)new TextFileSettings(customSettings.FullName)
                : new AppSettings();
        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            //Return default.cshtml home page for all 404 requests so we can handle routing on the client
            base.CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/default.cshtml");

            SetConfig( new HostConfig
            {
                DebugMode = AppSettings.Get("DebugMode",false),
                AddRedirectParamsToQueryString = true
            });

            this.Plugins.Add(new RazorFormat());
        }
    }
}
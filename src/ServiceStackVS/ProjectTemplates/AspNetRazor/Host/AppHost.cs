using System;
using System.Collections.Generic;
using Funq;
using $safeprojectname$.ServiceInterface;
using ServiceStack.Razor;
using ServiceStack;
using ServiceStack.Html;

namespace $safeprojectname$
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get("DebugMode", false),
                WebHostPhysicalPath = MapProjectPath("~/wwwroot"),
                UseCamelCase = true,
            });

            Plugins.Add(new RazorFormat());

            if (Config.DebugMode)
            {
                Plugins.Add(new HotReloadFeature());
            }
        }
    }

    //TODO: remove from v5.6.1
    public static class HtmlExtensions
    {
        public static HtmlString Navbar(this HtmlHelper html) => html.NavBar(ViewUtils.NavItems, null);
        
        public static HtmlString BundleJs(this HtmlHelper html, BundleOptions options) => ViewUtils.BundleJs(
            nameof(BundleJs), HostContext.VirtualFileSources, HostContext.VirtualFiles, Minifiers.JavaScript, options).ToHtmlString();

        public static HtmlString BundleCss(this HtmlHelper html, BundleOptions options) => ViewUtils.BundleCss(
            nameof(BundleCss), HostContext.VirtualFileSources, HostContext.VirtualFiles, Minifiers.Css, options).ToHtmlString();
    }
}

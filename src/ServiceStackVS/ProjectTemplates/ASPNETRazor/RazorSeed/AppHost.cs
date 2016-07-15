using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using ServiceStack.CSharp.RazorSeed.ServiceInterface;
using ServiceStack.Razor;
using ServiceStack;

namespace ServiceStack.CSharp.RazorSeed
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base class requires a Name and Assembly to locate web service implementation
        /// </summary>
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly) {}

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            this.Plugins.Add(new RazorFormat());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Funq;
using ServiceStack;
using ServiceStack.Mvc;
using $safeprojectname$.ServiceInterface;


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
                HandlerFactoryPath = "api",
            });
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
        }
    }
}

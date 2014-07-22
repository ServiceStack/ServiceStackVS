using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using $saferootprojectname$.ServiceInterface;
using ServiceStack.Razor;
using ServiceStack;

namespace $safeprojectname$
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("ServiceStack.CSharp.AngularJS Services", typeof(MyServices).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            AddPlugin(new RazorFormat());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using ServiceStack;
using ServiceStack.Razor;
using $saferootprojectname$.ServiceInterface;

namespace $safeprojectname$
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("ServiceStack.CSharp.Bootstrap Services", typeof(MyServices).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            AddPlugin(new RazorFormat());
        }
    }
}
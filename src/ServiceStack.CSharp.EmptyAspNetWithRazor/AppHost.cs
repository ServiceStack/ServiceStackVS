using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using ServiceStack.Razor;
using ServiceStack;

namespace ServiceStack.CSharp.EmptyAspNetWithRazor
{
    public class AppHost : AppHostBase
    {
        public AppHost() 
            : base("Service name", typeof(AppHost).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            AddPlugin(new RazorFormat());
        }
    }
}
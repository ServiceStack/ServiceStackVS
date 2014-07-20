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
        public AppHost()
            : base("ServiceStack.CSharp.RazorSeed Services", typeof(HelloWorldService).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            AddPlugin(new RazorFormat());
        }
    }
}
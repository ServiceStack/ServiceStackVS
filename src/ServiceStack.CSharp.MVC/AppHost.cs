using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Funq;
using ServiceStack;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceStack.CSharp.MVC.Models;


namespace ServiceStack.CSharp.MVC
{
    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Service name", typeof(AppHost).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            this.AddPlugin(new PostmanFeature());

            container.Register(ApplicationDbContext.Create());
            container.Register(new UserStore<ApplicationUser>(container.Resolve<ApplicationDbContext>()));
        }
    }
}
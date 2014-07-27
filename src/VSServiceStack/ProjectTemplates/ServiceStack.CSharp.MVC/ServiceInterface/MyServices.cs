using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceStack;
using ServiceStack.CSharp.MVC.Models;
using ServiceStack.CSharp.MVC.ServiceModel;

namespace ServiceStack.CSharp.MVC.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }
}
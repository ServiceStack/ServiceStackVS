using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.CSharp.AngularJS.ServiceModel;

namespace ServiceStack.CSharp.AngularJS.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}".Fmt(request.Name) };
        }
    }
}
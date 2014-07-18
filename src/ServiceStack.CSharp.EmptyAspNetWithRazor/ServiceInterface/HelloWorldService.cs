using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.CSharp.EmptyAspNetWithRazor.ServiceModel;

namespace ServiceStack.CSharp.EmptyAspNetWithRazor.ServiceInterface
{
    public class HelloWorldService : Service
    {
        public HelloWorldResponse Get(HelloWorldRequest request)
        {
            return request.ConvertTo<HelloWorldResponse>();
        }
    }
}
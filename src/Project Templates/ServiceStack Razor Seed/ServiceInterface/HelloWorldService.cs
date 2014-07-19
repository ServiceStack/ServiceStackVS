using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.CSharp.RazorSeed..ServiceModel;

namespace ServiceStack.CSharp.RazorSeed..ServiceInterface
{
    public class HelloWorldService : Service
    {
        public HelloWorldResponse Get(HelloWorldRequest request)
        {
            return request.ConvertTo<HelloWorldResponse>();
        }
    }
}
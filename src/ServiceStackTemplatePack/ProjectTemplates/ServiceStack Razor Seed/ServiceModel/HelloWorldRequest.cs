using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace $safeprojectname$
{
    [Route("/hello/{Name}")]
    public class HelloWorldRequest : IReturn<HelloWorldResponse>
    {
        public string Name { get; set; }
    }

    public class HelloWorldResponse
    {
        public string Name { get; set; }
    }
}
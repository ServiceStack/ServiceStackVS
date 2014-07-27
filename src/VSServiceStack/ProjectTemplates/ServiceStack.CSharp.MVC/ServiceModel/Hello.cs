using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ServiceStack.CSharp.MVC.ServiceModel
{
    public class Hello
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Result { get; set; }
    }
}
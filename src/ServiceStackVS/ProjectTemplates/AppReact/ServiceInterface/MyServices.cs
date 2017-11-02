using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Templates;
using ServiceStack.DataAnnotations;
using $saferootprojectname$.ServiceModel;

namespace $safeprojectname$
{
    [Exclude(Feature.Metadata)]
    [FallbackRoute("/{PathInfo*}")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }

    public class MyServices : Service
    {
        //Return index.html for unmatched requests so routing is handled on client
        public object Any(FallbackForClientRoutes request) => 
            new PageResult(Request.GetPage("/"));
            //new HttpResult(VirtualFileSources.GetFile("index.html")); //Use instead if index.html is just static HTML

        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}

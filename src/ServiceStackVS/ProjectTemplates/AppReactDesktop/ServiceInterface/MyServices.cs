using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
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
        //Return default.html for unmatched requests so routing is handled on client
        public object Any(FallbackForClientRoutes request) => 
            new HttpResult(VirtualFileSources.GetFile("index.html"));

        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
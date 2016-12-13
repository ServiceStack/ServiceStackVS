using System;
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
        public object Any(FallbackForClientRoutes request)
        {
            //Return default.html for unmatched requests so routing is handled on client
            return new HttpResult(VirtualFileSources.GetFile("default.html"));
        }

        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }
}
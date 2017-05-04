using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using $saferootprojectname$.ServiceModel;

namespace $safeprojectname$
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
        
        private static string defaultHtml = null;
        
        public object Any(FallbackForClientRoutes request)
        {
            return defaultHtml ?? 
                (defaultHtml = HostContext.ResolveVirtualFile("/default.html", Request).ReadAllText());
        }
    }

    [FallbackRoute("/{PathInfo*}")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }
}
using System;
using System.Linq;
using System.Net;
using Funq;
using ServiceStack;
using $saferootprojectname$.Resources;
using $saferootprojectname$.ServiceInterface;

namespace $safeprojectname$
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly) {}

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //Plugins.Add(new CorsFeature());

            SetConfig(new HostConfig {
                DebugMode = true,
                EmbeddedResourceBaseTypes = { typeof(AppHost), typeof(SharedEmbeddedResources) },
            });

            // This route is added using Routes.Add and ServiceController.RegisterService due to
            // using ILMerge limiting our AppHost : base() call to one assembly.
            // If two assemblies are used, the base() call searchs the same assembly twice due to the ILMerged result.
            Routes.Add<NativeHostAction>("/nativehost/{Action}");
            ServiceController.RegisterService(typeof(NativeHostService));
        }
    }

    public class NativeHostService : Service
    {
        public void Any(NativeHostAction request)
        {
            if (string.IsNullOrEmpty(request.Action))
                throw HttpError.NotFound("Function Not Found");

            var nativeHost = typeof(NativeHost).CreateInstance<NativeHost>();
            var methodName = request.Action.Substring(0, 1).ToUpper() + request.Action.Substring(1);
            var methodInfo = typeof(NativeHost).GetMethod(methodName);
            if (methodInfo == null)
                throw new HttpError(HttpStatusCode.NotFound, "Function Not Found");

            methodInfo.Invoke(nativeHost, null);
        }
    }

    public class NativeHostAction : IReturnVoid
    {
        public string Action { get; set; }
    }

    public class NativeHost
    {
        public void Quit()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
            {
                System.Threading.Thread.Sleep(10);    // Allow /nativehost/quit to return gracefully
                Environment.Exit(0);
            });
        }
    }
}

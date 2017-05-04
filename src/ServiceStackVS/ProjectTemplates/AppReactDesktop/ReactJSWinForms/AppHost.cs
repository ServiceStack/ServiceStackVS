using System;
using System.Threading.Tasks;
using System.IO;
using Funq;
using ServiceStack;
using Squirrel;
using $saferootprojectname$.Resources;
using $saferootprojectname$.ServiceInterface;

namespace $safeprojectname$
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("$safeprojectname$", typeof(MyServices).Assembly) {}

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig {
                DebugMode = true,
                EmbeddedResourceBaseTypes = { typeof(AppHost), typeof(SharedEmbeddedResources) },
                UseCamelCase = true,
            });
        }
    }
}

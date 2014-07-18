using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceStack.CSharp.MVC.Startup))]
namespace ServiceStack.CSharp.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

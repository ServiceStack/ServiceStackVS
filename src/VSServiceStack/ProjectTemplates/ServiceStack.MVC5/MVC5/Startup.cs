using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceStack.CSharp.MVC.Startup))]
namespace $safeprojectname$
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

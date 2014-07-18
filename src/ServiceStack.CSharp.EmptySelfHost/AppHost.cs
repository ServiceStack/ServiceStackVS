using Funq;
using ServiceStack;

namespace ServiceStack.CSharp.EmptySelfHost
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// Below defaults to same assembly as web host.
        /// </summary>
        public AppHost()
            : base("Service name", typeof(AppHost).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.AddPlugin(new PostmanFeature());
            //this.AddPlugin(new CorsFeature());
        }
    }
}

using System;
using ServiceStack;
using Funq;
using ServiceStack.Razor;
using ${SolutionName}.ServiceInterface;
using ${SolutionName}.Resources;

namespace ${Namespace}
{
	public class AppHost : AppSelfHostBase
	{
		/// <summary>
		/// Default constructor.
		/// Base constructor requires a name and assembly to locate web service classes. 
		/// </summary>
		public AppHost()
			: base("${SolutionName}", typeof(MyServices).Assembly)
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
			//this.Plugins.Add(new PostmanFeature());
			//Plugins.Add(new CorsFeature());

			Plugins.Add(new RazorFormat
				{
					LoadFromAssemblies = { typeof(CefResources).Assembly },
				});

			SetConfig(new HostConfig
				{
					DebugMode = true,
					EmbeddedResourceBaseTypes = { typeof(AppHost), typeof(CefResources) },
				});
		}
	}
}


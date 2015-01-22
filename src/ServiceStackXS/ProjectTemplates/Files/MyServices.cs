using ServiceStack;
using ${SolutionName}.ServiceModel;

namespace ${Namespace}
{
	public class MyServices : Service
	{
		public object Any(Hello request)
		{
			return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
		}
	}
}
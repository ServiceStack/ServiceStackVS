using ServiceStack;
using $saferootprojectname$.ServiceModel;

namespace $safeprojectname$
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
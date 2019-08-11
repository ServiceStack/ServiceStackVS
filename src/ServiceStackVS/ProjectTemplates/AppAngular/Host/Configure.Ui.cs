using System.Collections.Generic;
using ServiceStack;

namespace $safeprojectname$
{
    public class ConfigureUi : IConfigureAppHost
    {
        public void Configure(IAppHost appHost)
        {
            // if wwwroot/ is empty, build Client App with 'npm run build'
            var svgDir = appHost.RootDirectory.GetDirectory("/svg"); 
            if (svgDir != null)
            {
                Svg.Load(svgDir);
            }

            View.NavItems.AddRange(new List<NavItem>
            {
                new NavItem { Href = "/", Label = "Home", Exact = true },
                new NavItem { Href = "/about", Label = "About" },
                new NavItem { Href = "/login", Label = "Sign In", Hide = "auth" },
                new NavItem { Href = "/profile", Label = "Profile", Show = "auth" },
                new NavItem { Href = "/admin", Label = "Admin", Show = "role:Admin" },
            });
        }
    }
}

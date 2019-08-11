using System.Net;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Razor;

namespace $safeprojectname$
{
    public class ConfigureUi : IConfigureAppHost
    {
        public void Configure(IAppHost appHost)
        {
            appHost.CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new RazorHandler("/notfound");
            appHost.CustomErrorHttpHandlers[HttpStatusCode.Forbidden] = new RazorHandler("/forbidden");

            Svg.Load(appHost.RootDirectory.GetDirectory("/assets/svg"));
            Svg.CssFillColor["svg-icons"] = "#343a40";

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


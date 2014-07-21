using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceStack;
using ServiceStack.CSharp.MVC.Models;
using ServiceStack.CSharp.MVC.ServiceModel;

namespace ServiceStack.CSharp.MVC.ServiceInterface
{
    public class UserService : Service
    {
        public UserStore<ApplicationUser> UserStore { get; set; }

        public UserDetailsResponse Get(UserDetailsRequest request)
        {
            var task = UserStore.FindByEmailAsync(request.EmailAddress);
            var user = task.Result;

            return user.ConvertTo<UserDetailsResponse>();
        }
    }
}
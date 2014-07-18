using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace ServiceStack.CSharp.MVC.ServiceModel
{
    [Route("/users/{EmailAddress}")]
    public class UserDetailsRequest : IReturn<UserDetailsResponse>
    {
        public string EmailAddress { get; set; }
    }

    public class UserDetailsResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
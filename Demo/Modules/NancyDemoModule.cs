using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Owin;
using Nancy.Security;

namespace Demo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            this.RequiresMSOwinAuthentication();

            Get["/nancy"] = x =>
            {
                var environment = Context.GetOwinEnvironment();
                var user = Context.GetMSOwinUser();
                return "Hello from Nancy ! You requested: " + environment["owin.RequestPath"] + "<br/><br/>User: " +
                       user.Identity.Name;
            };
        }
    }
}
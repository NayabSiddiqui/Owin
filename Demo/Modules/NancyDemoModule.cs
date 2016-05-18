using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Owin;

namespace Demo.Modules
{
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            Get["/nancy"] = x =>
            {
                var environment = Context.GetOwinEnvironment();
                return "Hello from Nancy ! You requested: " + environment["owin.RequestPath"];
            };
        }
    }
}
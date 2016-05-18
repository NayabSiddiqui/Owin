using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using Demo.Middleware;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Nancy;
using Nancy.Owin;
using Owin;

namespace Demo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
           /* app.Use<DebugMiddleware>(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (context) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    context.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (context) =>
                {
                    var watch = (Stopwatch) context.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }
            });*/

            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (context) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    context.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (context) =>
                {
                    var watch = (Stopwatch)context.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login")
            });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "1624010771256816",
                AppSecret = "a599b8a0ac81e55aa3ca669e4157d705",
                SignInAsAuthenticationType = "ApplicationCookie"
            });

            app.Use(async (context, next) =>
            {
                if (context.Authentication.User.Identity.IsAuthenticated)
                    Debug.WriteLine("User: " + context.Authentication.User.Identity.Name);
                else
                    Debug.WriteLine("User not authenticated !");

                await next();
            });

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfiguration);

            app.Map("/nancy", mappedApp => mappedApp.UseNancy());
            //            app.UseNancy(config => config.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound));

            // TODO: commented out so that the routes can default back to MVC
            /*  app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<html>" +
                                                  "<head></head>" +
                                                  "<body>" +
                                                  "Hello World" +
                                                  "</body>" +
                                                  "</html>");
            });*/
        }
    }
}
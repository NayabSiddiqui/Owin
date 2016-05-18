using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using Demo.Middleware;
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

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            app.UseWebApi(httpConfiguration);

            app.UseNancy(config => config.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound));

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;

namespace Demo.Middleware
{
    public static class Extensions
    {
        public static void UseDebugMiddleware(this IAppBuilder app, DebugMiddlewareOptions options = null)
        {
            if (options == null)
            {
                options = new DebugMiddlewareOptions();
            }

            app.Use<DebugMiddleware>(options);
        }
    }
}
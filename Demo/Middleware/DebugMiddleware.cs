using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using AppFunc = System.Func<
    System.Collections.Generic.IDictionary<string, object>,
    System.Threading.Tasks.Task
>;

namespace Demo.Middleware
{
    public class DebugMiddleware
    {
        private readonly AppFunc _next;
        private readonly DebugMiddlewareOptions _options;

        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            _next = next;
            _options = options;

            if (_options.OnIncomingRequest == null)
            {
                _options.OnIncomingRequest =
                    (context) => Debug.WriteLine("Incoming request: " + context.Request.Path);
            }
            if (_options.OnOutgoingRequest == null)
            {
                _options.OnOutgoingRequest =
                    (context) => Debug.WriteLine("Outgoing request: " + context.Request.Path);
            }
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var context = new OwinContext(environment);
            _options.OnIncomingRequest(context);
            await _next(environment);
            _options.OnOutgoingRequest(context);
        }
    }
}
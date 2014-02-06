using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace PUrify.IntegrationTests.Clients
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using System.Net;
    public class OwinStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseHandler(async (req, res) =>
            {
                var context = req.Dictionary.Values.OfType<HttpListenerContext>().FirstOrDefault();
                var path = context.Request.RawUrl;
                var bytes = Encoding.UTF8.GetBytes(path);
                await res.Body.WriteAsync(bytes, 0, bytes.Length);
            });
        }
    }
}

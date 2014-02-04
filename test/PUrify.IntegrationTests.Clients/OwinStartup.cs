using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace PUrify.IntegrationTests.Clients
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class OwinStartup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseHandler(async (req, res) =>
            {
                var bytes = Encoding.UTF8.GetBytes(req.Path);
                await req.Body.WriteAsync(bytes, 0, bytes.Length);
            });
        }
    }
}

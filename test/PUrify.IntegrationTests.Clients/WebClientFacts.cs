using Microsoft.Owin.Hosting;
using System.Net;
using System.Threading.Tasks;
using Should;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class WebClientFacts 
    {
        public class DownloadStringMethod
        {
            private readonly string _body;
            public DownloadStringMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new WebClient();
                    _body = client.DownloadString(RequestConfiguration.Uri);
                } 
            }
            [Fact]
            public void ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }
    }
}
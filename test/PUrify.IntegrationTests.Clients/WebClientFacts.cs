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
            [Fact]
            public void ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new WebClient();
                    var body = client.DownloadString(RequestConfiguration.Uri);
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }
    }
}
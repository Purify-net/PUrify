using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class WebClientTests : RequestTestsBase
    {
        [Fact]
        public async Task WebClient()
        {
            var uri = this.CreateUri(this._path);
            var client = new WebClient();
            var body = client.DownloadString(uri);
            Assert.Equal(body, this._path);
        }
    }
}
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Should;
using Xunit;
using Microsoft.Owin.Hosting;

namespace PUrify.IntegrationTests.Clients
{
    public class HttpClientTestsBase : RequestTestsBase
    {
        [Fact]
        public async Task GetAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                var result = await client.GetAsync(uri);
                Assert.True(result.IsSuccessStatusCode);
                var body = await result.Content.ReadAsStringAsync();
                body.ShouldEqual(this._path);
            }
        }

        [Fact]
        public async Task PostAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                var result = await client.PostAsync(uri, new StringContent("ads"));
                Assert.True(result.IsSuccessStatusCode);
                var body = await result.Content.ReadAsStringAsync();
                body.ShouldEqual(this._path);
            }
        }

        [Fact]
        public async Task PutAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                var result = await client.PutAsync(uri, new StringContent("ads"));
                Assert.True(result.IsSuccessStatusCode);
                var body = await result.Content.ReadAsStringAsync();
                body.ShouldEqual(this._path);
            }
        }

        [Fact]
        public async Task GetStreamAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                using (var result = await client.GetStreamAsync(uri))
                using (var sr = new StreamReader(result))
                {
                    var body = await sr.ReadToEndAsync();
                    body.ShouldEqual(this._path);
                }
            }
        }

        [Fact]
        public async Task GetStringAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                var body = await client.GetStringAsync(uri);
                body.ShouldEqual(this._path);
            }
        }

        [Fact]
        public async Task DeleteAsync()
        {
            using (WebApp.Start<OwinStartup>("http://localhost:5000"))
            {
                var client = new HttpClient();
                var uri = this.CreateUri(this._path);
                var result = await client.DeleteAsync(uri);
                Assert.True(result.IsSuccessStatusCode);
                var body = await result.Content.ReadAsStringAsync();
                body.ShouldEqual(this._path);
            }
        }
    }
}
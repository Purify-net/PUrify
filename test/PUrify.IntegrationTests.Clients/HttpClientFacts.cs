using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Should;
using Xunit;
using Microsoft.Owin.Hosting;

namespace PUrify.IntegrationTests.Clients
{
    public class HttpClientFacts
    {
        public class GetAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new HttpClient();
                    var result = await client.GetAsync(RequestConfiguration.Uri);
                    var body = await result.Content.ReadAsStringAsync();
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }

        public class PostAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new HttpClient();
                    var result = await client.PostAsync(RequestConfiguration.Uri, new StringContent("ads"));
                    var body = await result.Content.ReadAsStringAsync();
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }

        public class PutAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new HttpClient();
                    var result = await client.PutAsync(RequestConfiguration.Uri, new StringContent("ads"));
                    var body = await result.Content.ReadAsStringAsync();
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }

        public class GetStreamAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new HttpClient();
                    using (var result = await client.GetStreamAsync(RequestConfiguration.Uri))
                    using (var sr = new StreamReader(result))
                    {
                        var body = await sr.ReadToEndAsync();
                        body.ShouldEqual(RequestConfiguration.Path);
                    }
                }
            }
        }

        public class GetStringAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Path))
                {
                    var client = new HttpClient();
                    var body = await client.GetStringAsync(RequestConfiguration.Uri);
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }

        public class DeleteAsyncMethod
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Path))
                {
                    var client = new HttpClient();
                    var result = await client.DeleteAsync(RequestConfiguration.Uri);
                    var body = await result.Content.ReadAsStringAsync();
                    body.ShouldEqual(RequestConfiguration.Path);
                }
            }
        }
    }
}
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Should;
using Xunit;
using Microsoft.Owin.Hosting;

namespace PUrify.IntegrationTests.Clients
{
    public class HttpWebRequestFacts
    {
        public class PostUsingIAsyncPattern
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "POST";
                    var requestStream = await Task.Factory.FromAsync<Stream>(client.BeginGetRequestStream, client.EndGetRequestStream, null);
                    using (requestStream)
                    {
                        var buffer = Encoding.UTF8.GetBytes("asd");
                        await Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, buffer, 0, buffer.Length, null);
                    }

                    var response = (HttpWebResponse) 
                        await Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null);
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        body.ShouldEqual(RequestConfiguration.Path);
                    }
                }
            }
        }

        public class GetUsingIAsyncPattern
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "GET";
                    var response = (HttpWebResponse)
                            await Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null);
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        body.ShouldEqual(RequestConfiguration.Path);
                    }
                }
            }
        }

        public class PostAsync
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "POST";
                    using (var requestStream = await client.GetRequestStreamAsync())
                    {
                        var buffer = Encoding.UTF8.GetBytes("asd");
                        await requestStream.WriteAsync(buffer, 0, buffer.Length);
                    }
                    using (var response = await client.GetResponseAsync())
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        body.ShouldEqual(RequestConfiguration.Path);
                    }
                }
            }
        }

        public class GetAsync
        {
            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "GET";
                    using (var response = await client.GetResponseAsync())
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        body.ShouldEqual(RequestConfiguration.Path);
                    }
                }
            }
        }
    }
}


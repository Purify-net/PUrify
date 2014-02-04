using System;
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
    public class HttpWebRequestTests
    {
        public class HttpWebRequestConstructors
        {
            private string _url = "http://localhost/%2F";

            [Fact]
            public void WeShouldRunTheseTestsUnderNET40()
            {
                var legacyV2Quirks = typeof (UriParser).GetProperty("ShouldUseLegacyV2Quirks",
                    BindingFlags.Static | BindingFlags.NonPublic);
                legacyV2Quirks.ShouldNotBeNull();

                var isBrokenUri = (bool) legacyV2Quirks.GetValue(null, null);
                isBrokenUri.ShouldBeTrue();
            }

            [Fact]
            public void TestUriConstructorOnWebRequest()
            {
                var uri = new Uri(_url);
                var webRequest = WebRequest.Create(uri);
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeFalse();

                uri = new Uri(_url);
                uri.Purify();
                webRequest = WebRequest.Create(uri);
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }

            [Fact]
            public void TestStringConstructorOnWebRequest()
            {
                var webRequest = WebRequest.Create(_url);
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeFalse();
                webRequest.RequestUri.Purify();
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }


            [Fact]
            public void TestUriConstructorOnWebRequest_CreateDefault()
            {
                var webRequest = WebRequest.CreateDefault(new Uri(_url));
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeFalse();
                webRequest.RequestUri.Purify();
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }
        }


        public class RequestTests : RequestTestsBase
        {
            [Fact]
            public async Task IAsyncPost()
            {
                using (WebApp.Start<OwinStartup>("http://localhost:5000"))
                {
                    var uri = this.CreateUri(this._path);
                    var client = (HttpWebRequest)WebRequest.Create(uri);
                    client.Method = "POST";
                    var requestStream = await Task.Factory.FromAsync<Stream>(client.BeginGetRequestStream, client.EndGetRequestStream, null);
                    using (requestStream)
                    {
                        var buffer = Encoding.UTF8.GetBytes("asd");
                        await Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, buffer, 0, buffer.Length, null);
                    }

                    var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null);
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        Assert.Equal(body, this._path);
                    }
                }
            }
            [Fact]
            public async Task IAsyncGet()
            {
                using (WebApp.Start<OwinStartup>("http://localhost:5000"))
                {
                    var uri = this.CreateUri(this._path);
                    var client = (HttpWebRequest)WebRequest.Create(uri);
                    client.Method = "GET";
                    var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null);
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        Assert.Equal(body, this._path);
                    }
                }
            }
            
            [Fact]
            public async Task PostAsync()
            {
                using (WebApp.Start<OwinStartup>("http://localhost:5000"))
                {
                    var uri = this.CreateUri(this._path);
                    var client = (HttpWebRequest)WebRequest.Create(uri);
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
                        Assert.Equal(body, this._path);
                    }
                }
            }
            
            [Fact]
            public async Task GetAsync()
            {
                using (WebApp.Start<OwinStartup>("http://localhost:5000"))
                {
                    var uri = this.CreateUri(this._path);
                    var client = (HttpWebRequest)WebRequest.Create(uri);
                    client.Method = "GET";
                    using (var response = await client.GetResponseAsync())
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var body = await sr.ReadToEndAsync();
                        Assert.Equal(body, this._path);
                    }
                }
            }
        }
    
    }
}

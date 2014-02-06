using System.CodeDom;
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
            private readonly string _body;

            public PostUsingIAsyncPattern()
            {
                 using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "POST";
                    var requestStream = Task.Factory.FromAsync<Stream>(client.BeginGetRequestStream, client.EndGetRequestStream, null).Result;
                    using (requestStream)
                    {
                        var buffer = Encoding.UTF8.GetBytes("asd");
                        var x = Task.Factory.FromAsync(requestStream.BeginWrite, requestStream.EndWrite, buffer, 0, buffer.Length, null);
                        x.GetAwaiter().GetResult();
                    }

                    var response = (HttpWebResponse) 
                        Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null).Result;
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        _body = sr.ReadToEndAsync().Result;
                    }
                }
               
            }

            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class GetUsingIAsyncPattern
        {
            private readonly string _body;
            public GetUsingIAsyncPattern()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "GET";
                    var response = (HttpWebResponse)
                            Task.Factory.FromAsync<WebResponse>(client.BeginGetResponse, client.EndGetResponse, null).Result;
                    using (response)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        _body = sr.ReadToEndAsync().Result;
                    }
                }     
            }

            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class PostAsync
        {
            private readonly string _body;

            public PostAsync()
            {
                 using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "POST";
                    using (var requestStream = client.GetRequestStreamAsync().Result)
                    {
                        var buffer = Encoding.UTF8.GetBytes("asd");
                        requestStream.WriteAsync(buffer, 0, buffer.Length).GetAwaiter().GetResult();
                    }
                    using (var response = client.GetResponseAsync().Result)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        _body = sr.ReadToEndAsync().Result;
                    }
                }    
            }

            [Fact]
            public async Task ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class GetAsync
        {
            private readonly string _body;

            public GetAsync()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = (HttpWebRequest) WebRequest.Create(RequestConfiguration.Uri);
                    client.Method = "GET";
                    using (var response = client.GetResponseAsync().Result)
                    using (var responseStream = response.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        _body = sr.ReadToEndAsync().Result;
                    }
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


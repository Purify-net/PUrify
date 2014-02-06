using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Should;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class HttpClientFacts
    {
        public class GetAsyncMethod
        {
            private readonly string _body;

            public GetAsyncMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new System.Net.Http.HttpClient();
                    var result = client.GetAsync(RequestConfiguration.Uri).Result;
                    _body = result.Content.ReadAsStringAsync().Result;
                }    
            }

            [Fact]
            public void ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class PostAsyncMethod
        {
            private readonly string _body;

            public PostAsyncMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new System.Net.Http.HttpClient();
                    var result = client.PostAsync(RequestConfiguration.Uri, new StringContent("ads")).Result;
                    _body = result.Content.ReadAsStringAsync().Result;
                } 
            }

            [Fact]
            public void ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class PutAsyncMethod
        {
            private readonly string _body;

            public PutAsyncMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new System.Net.Http.HttpClient();
                    var result = client.PutAsync(RequestConfiguration.Uri, new StringContent("ads")).Result;
                    _body = result.Content.ReadAsStringAsync().Result;
                }     
            }

            [Fact]
            public void ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class GetStreamAsyncMethod
        {
            private readonly string _body;

            public GetStreamAsyncMethod()
            {
                 using (WebApp.Start<OwinStartup>(RequestConfiguration.Url))
                {
                    var client = new System.Net.Http.HttpClient();
                    using (var result = client.GetStreamAsync(RequestConfiguration.Uri).Result)
                    using (var sr = new StreamReader(result))
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

        public class GetStringAsyncMethod
        {
            private readonly string _body;

            public GetStringAsyncMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Path))
                {
                    var client = new System.Net.Http.HttpClient();
                    _body = client.GetStringAsync(RequestConfiguration.Uri).Result;
                }
             
            }

            [Fact]
            public void ReturnsPathInResponseBody()
            {
                _body.ShouldEqual(RequestConfiguration.Path);
            }
        }

        public class DeleteAsyncMethod
        {
            private readonly string _body;

            public DeleteAsyncMethod()
            {
                using (WebApp.Start<OwinStartup>(RequestConfiguration.Path))
                {
                    var client = new System.Net.Http.HttpClient();
                    var result = client.DeleteAsync(RequestConfiguration.Uri).Result;
                    _body = result.Content.ReadAsStringAsync().Result;
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
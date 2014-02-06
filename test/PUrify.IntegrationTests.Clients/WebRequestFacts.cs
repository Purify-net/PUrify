using System;
using System.Net;
using Should;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class WebRequestFacts
    {
        public class FactoryCreate
        {
            private readonly Uri _uri = new Uri("http://localhost/%2F").Purify();

            [Fact]
            public void UriArgument_DoesNotLoosePurifiedUri()
            {
                var webRequest = WebRequest.Create(this._uri);
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }
        }

        public class FactoryCreateDefault
        {
            private readonly Uri _uri = new Uri("http://localhost/%2F").Purify();

            [Fact]
            public void UriArgument_DoesNotLoosPurifiedUri()
            {
                var webRequest = WebRequest.CreateDefault(this._uri);
                webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }
        }
    }
}
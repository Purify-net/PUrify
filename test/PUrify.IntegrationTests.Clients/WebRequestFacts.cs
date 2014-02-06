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
            private readonly Uri _uri; 

            public FactoryCreate()
            {
                var uriGoingIn = new Uri("http://localhost/%2F").Purify();
                var webRequest = WebRequest.Create(uriGoingIn);
                _uri = webRequest.RequestUri;
            }

            [Fact]
            public void UriArgument_DoesNotLoosePurifiedUri()
            {
                _uri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }
        }

        public class FactoryCreateDefault
        {
            private readonly Uri _uri;

            public FactoryCreateDefault()
            {
                var uriGoingIn = new Uri("http://localhost/%2F").Purify();
                var webRequest = WebRequest.CreateDefault(uriGoingIn);
                _uri = webRequest.RequestUri;
            }

            [Fact]
            public void UriArgument_DoesNotLoosPurifiedUri()
            {
                _uri.AbsoluteUri.EndsWith("/%2F").ShouldBeTrue();
            }
        }
    }
}
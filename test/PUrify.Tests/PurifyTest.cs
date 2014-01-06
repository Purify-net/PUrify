using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Purify;
using Xunit;
using Should;

namespace PUrify.Tests
{
    public class PurifyTest
    {
      
        public class ThePurifyMethod
        {
            private readonly Uri _uri;

            public ThePurifyMethod()
            {
                _uri = new Uri("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
                _uri.Purify();
            }

            [Fact]
            public void ToStringContainsEscapedSlashes()
            {
                _uri.ToString().ShouldEqual("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
            }

            [Fact]
            public void AbsoluteUriContainsEscapedSlashes()
            {
                _uri.AbsoluteUri.ShouldEqual("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
            }

            [Fact]
            public void QueryContainsEscapedSlashes()
            {
                _uri.Query.ShouldEqual("?Foo=Bar%2F");
            }

            [Fact]
            public void PathAndQueryContainsEscapedSlashes()
            {
                _uri.PathAndQuery.ShouldEqual("/%2F?Foo=Bar%2F");
            }

            [Fact]
            public void AbsolutePathContainsEscapedSlashes()
            {
                _uri.AbsolutePath.ShouldEqual("/%2F");
            }

            [Fact]
            public void UriFragmentIsProperlySet()
            {
                _uri.Fragment.ShouldEqual("#frag");
            }

            [Fact]
            public void DoesNotThrowIfNoQueryIsSpecified()
            {
                var uri = new Uri("http://localhost/%2F");
                Assert.DoesNotThrow(uri.Purify);
                uri.AbsoluteUri.ShouldEqual("http://localhost/%2F");
                uri.AbsolutePath.ShouldEqual("/%2F");
            }

        }

    }
}

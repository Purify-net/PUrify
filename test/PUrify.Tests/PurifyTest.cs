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
            private readonly string _url = "http://localhost/%2F";
            private string _rewriteRelativeUrl = "../%2F" + "-rewrite";
            private string _rewriteAbsoluteUrl;

            public ThePurifyMethod()
            {
                _uri = new Uri("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
                _uri.Purify();
                _rewriteAbsoluteUrl = _url + "-rewrite";
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
                var uri = new Uri(_url);
                Assert.DoesNotThrow(uri.Purify);
                uri.AbsoluteUri.ShouldEqual(_url);
                uri.AbsolutePath.ShouldEqual("/%2F");
            }

            [Fact]
            public void CanHandleAbsoluteUri()
            {
                var uri = new Uri(_url, UriKind.Absolute);
                Assert.DoesNotThrow(uri.Purify);
                uri.AbsoluteUri.ShouldEqual(_url);
            }

            [Fact]
            public void CanHandleRelativeUri()
            {
                var uri = new Uri("../something", UriKind.Relative);
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual("../something");
            }

            [Fact]
            public void CanHandleAbsoluteOrRelativeUri_RelativeGiven()
            {
                var uri = new Uri("../something", UriKind.RelativeOrAbsolute);
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual("../something");
            }

            [Fact]
            public void CanHandleAbsoluteOrRelativeUri_AbsoluteGiven()
            {
                var uri = new Uri(_url, UriKind.RelativeOrAbsolute);
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual(_url);
            }


            [Fact]
            public void CanJoinRelativeOntoAbsolute()
            {
                var uri = new Uri(new Uri(_url), new Uri(_rewriteRelativeUrl, UriKind.Relative));
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual(_rewriteAbsoluteUrl);
            }

            [Fact]
            public void CanJoinAbsoluteOntoAbsolute()
            {
                var uri = new Uri(new Uri(_url), new Uri(_rewriteAbsoluteUrl, UriKind.Absolute));
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual(_rewriteAbsoluteUrl);
            }

            [Fact]
            public void CanJoinRelativeOntoAbsolute_StringGiven()
            {
                var uri = new Uri(new Uri(_url), _rewriteRelativeUrl);
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual(_rewriteAbsoluteUrl);
            }

            [Fact]
            public void CanJoinAbsoluteOntoAbsolute_StringGiven()
            {
                var uri = new Uri(new Uri(_url), _rewriteAbsoluteUrl);
                Assert.DoesNotThrow(uri.Purify);
                uri.ToString().ShouldEqual(_rewriteAbsoluteUrl);
            }
        }

    }
}

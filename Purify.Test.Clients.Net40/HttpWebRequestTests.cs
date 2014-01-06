using System;
using System.Net;
using System.Reflection;
using Should;
using Xunit;

namespace Purify.Test.Clients.Net40
{
    public class HttpWebRequestTests
    {
        [Fact]
        public void WeShouldRunTheseTestsUnderNET40()
        {
            var legacyV2Quirks = typeof(UriParser).GetProperty("ShouldUseLegacyV2Quirks", BindingFlags.Static | BindingFlags.NonPublic);
            legacyV2Quirks.ShouldNotBeNull();

            var isBrokenUri = (bool)legacyV2Quirks.GetValue(null, null);
            isBrokenUri.ShouldBeTrue();
        }

        [Fact]
        public void TestSlash()
        {
            var uri = new Uri("http://localhost/%2F");
            var webRequest = WebRequest.Create(uri);
            webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeFalse();

            uri = new Uri("http://localhost/%2F");
            uri.Purify();
            webRequest = WebRequest.Create(uri);
            webRequest.RequestUri.AbsoluteUri.EndsWith("/%2F").ShouldBeFalse();
        }
     

    }
}

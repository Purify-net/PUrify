using System;
using Purify;
using Xunit;

namespace PUrify.Tests
{
    public class UriInfoTest
    {
        public class Constructors
        {
            [Fact]
            public void UriInfoWithoutQueryDoesNotThrows()
            {
                Assert.DoesNotThrow(() => new UriInfo(new Uri("http://localhost/%2F"), "http://localhost/%2F"));
            }
            [Fact]
            public void UriInfoWithoutPathDoesNotThrows()
            {
                Assert.DoesNotThrow(() => new UriInfo(new Uri("http://localhost/"), "http://localhost/"));
            }
        }
    }
}
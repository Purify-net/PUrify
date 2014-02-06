using System;
using System.Reflection;
using Should;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class UriBehaviourFacts
    {
        public class ShouldUseLegacyV2Quirks
        {
            private readonly bool _isBrokenUri;
            public ShouldUseLegacyV2Quirks()
            {
                var legacyV2Quirks = typeof (UriParser).GetProperty("ShouldUseLegacyV2Quirks", BindingFlags.Static | BindingFlags.NonPublic);
                legacyV2Quirks.ShouldNotBeNull();

                _isBrokenUri = (bool) legacyV2Quirks.GetValue(null, null);
            }

            [Fact]
            public void ShouldReturnTrue_OtherwiseWeAreNotTestingTheProperDotNetVersion()
            {
                _isBrokenUri.ShouldBeTrue();
            }
        }
    }
}
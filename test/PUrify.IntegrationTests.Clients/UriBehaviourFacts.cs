﻿using System;
using System.Reflection;
using Should;
using Xunit;

namespace PUrify.IntegrationTests.Clients
{
    public class UriBehaviourFacts
    {
        public class ShouldUseLegacyV2Quirks
        {
            [Fact]
            public void ShouldReturnTrue_OtherwiseWeAreNotTestingTheProperDotNetVersion()
            {
                var legacyV2Quirks = typeof (UriParser).GetProperty("ShouldUseLegacyV2Quirks",
                    BindingFlags.Static | BindingFlags.NonPublic);
                legacyV2Quirks.ShouldNotBeNull();

                var isBrokenUri = (bool) legacyV2Quirks.GetValue(null, null);
                isBrokenUri.ShouldBeTrue();
            }
        }
    }
}
using System;
using System.Reflection;
using NUnit;
using NUnit.Framework;

namespace Purify.Test.Client.Net4
{
    [TestFixture]
    public class HttpWebRequestTests
    {
        [Test]
        public void WeShouldRunTheseTestsUnderNET40()
        {
            var legacyV2Quirks = typeof(UriParser).GetProperty("ShouldUseLegacyV2Quirks", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.NotNull(legacyV2Quirks);

            var isBrokenUri = (bool)legacyV2Quirks.GetValue(null, null);
            Assert.True(isBrokenUri);
        }
     
    }
}

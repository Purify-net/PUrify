using System;
using Purify;
using Should;
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
       
        public class ConstructorWithNonDefaultPort
        {
            private UriInfo _uriInfo;
            private Uri _uri;

            public ConstructorWithNonDefaultPort()
            {
                var url = "http://localhost:1234/";
                _uriInfo = new UriInfo(new Uri(url), url);
                _uri = new Uri(url).Purify();
            }

            [Fact]
            public void PortShouldNotBePartOfThePath()
            {
                _uriInfo.Path.ShouldEqual("/");
            }
            [Fact]
            public void PortShouldNotHaveBeenLost()
            {
                _uri.Port.ShouldEqual(1234);
            }
        }
        
        public class ConstructorWithDefaultPort
        {
            private UriInfo _uriInfo;
            private Uri _uri;

            public ConstructorWithDefaultPort()
            {
                var url = "http://localhost:80/";
                _uriInfo = new UriInfo(new Uri(url), url);
                _uri = new Uri(url).Purify();
            }

            [Fact]
            public void PortShouldNotBePartOfThePath()
            {
                _uriInfo.Path.ShouldEqual("/");
            }
            
            [Fact]
            public void PortShouldNotHaveBeenLost()
            {
                _uri.Port.ShouldEqual(80);
            }
        }

        public class ConstructorWithPortAndUserNameInfo
        {
            private UriInfo _uriInfo;
            private Uri _uri;

            public ConstructorWithPortAndUserNameInfo()
            {
                var url = "http://mpdreamz:hasapassword@localhost:80/";
                _uriInfo = new UriInfo(new Uri(url), url);
                _uri = new Uri(url).Purify();
            }

            [Fact]
            public void PortShouldNotBePartOfThePath()
            {
                _uriInfo.Path.ShouldEqual("/");
            }
            
            [Fact]
            public void UserInfoShouldNotHaveBeenLost()
            {
                _uri.UserInfo.ShouldEqual("mpdreamz:hasapassword");
            }
        }
    }
}
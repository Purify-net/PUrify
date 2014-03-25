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


        public class ConstructorWithOnlyQueryString
        {
            private UriInfo _uriInfo;
            private Uri _uri;

            public ConstructorWithOnlyQueryString()
            {
                var path = "?authToken=ABCDEFGHIJK";
                var url = "http://localhost";
                var uri = new Uri(url);
                _uriInfo = new UriInfo(new Uri(uri, path).Purify(), url + path);
                _uri = new Uri(uri, path).Purify();
            }

            [Fact]
            public void ShouldStartPathWithAForwardSlash()
            {
                _uriInfo.Source.ShouldEqual("http://localhost/?authToken=ABCDEFGHIJK");
            }

            [Fact]
            public void UriToStringShouldBeEqualToUriInfoSource()
            {
                _uri.ToString().ShouldEqual(_uriInfo.Source);
            }

            [Fact]
            public void PortShouldNotBePartOfThePath()
            {
                _uri.AbsolutePath.ShouldEqual("/");
            }

            [Fact]
            public void PathShouldContainQueryString()
            {
                _uri.PathAndQuery.ShouldEqual("/?authToken=ABCDEFGHIJK");
            }  

            [Fact]
            public void QueryStringPropertyShouldNotBeBlank()
            {
                _uri.Query.ShouldEqual("?authToken=ABCDEFGHIJK");
            }
        }
    }
}
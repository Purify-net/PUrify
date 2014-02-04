using System;

namespace PUrify.IntegrationTests.Clients
{
    public class RequestTestsBase
    {
        private readonly string _url = "http://localhost:5000";
        protected readonly string _path = "/hello%2F/world-%2f/";

        protected Uri CreateUri(string path)
        {
            if (!path.StartsWith("/"))
                throw new Exception("path should begin with a '/': " + path);
            var uri = new Uri(this._url + path);
            uri.Purify();
            return uri;
        }
    }
}
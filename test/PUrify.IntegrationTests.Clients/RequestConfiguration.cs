using System;

namespace PUrify.IntegrationTests.Clients
{
    public class RequestConfiguration
    {
        public static readonly string Url = "http://localhost:5000";
        public static readonly string Path = "/hello%2F/world-%2f/";
        public static readonly Uri Uri = new Uri(Url + Path).Purify();
    }
}
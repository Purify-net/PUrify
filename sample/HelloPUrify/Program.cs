using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Purify;

namespace HelloPUrify
{
    class Program
    {
        static void Main(string[] args)
        {
            string uriString = "http://www.myapi.com/%2F?Foo=Bar%2F#frag";
            var uri = new Uri(uriString);
            Console.WriteLine("Uri String\n\t" + uriString);
            Console.WriteLine("Uri Before PUrify:");
            ShowUriDetails(uri);           
            uri.Purify();
            Console.WriteLine("Uri After PUrify:");
            ShowUriDetails(uri);
            Console.ReadLine();
        }

        public static void ShowUriDetails(Uri uri)
        {
            Console.WriteLine("\turi.ToString() - " + uri.ToString());
            Console.WriteLine("\turi.AbsoluteUri - " + uri.AbsoluteUri);
            Console.WriteLine("\turi.Host - " + uri.Host);
            Console.WriteLine("\turi.Query - " + uri.Query);
            Console.WriteLine("\turi.PathAndQuery - " + uri.PathAndQuery);
            Console.WriteLine("\turi.AbsolutePath - " + uri.AbsolutePath);
            Console.WriteLine("\turi.Fragment - " + uri.Fragment);
        }
    }
}

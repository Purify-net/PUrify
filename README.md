PUrify - Your Uris have been PUrified!
======

# Why do I need my Uris purified?

The Uri classes in .NET prior to 4.5 and Mono scrub through your Uris and modify them in order to prevent vulnerabilities, for example escaped slashes are unescaped. This scrubbing however prevents Uris that are inline with [RFC 3986] (http://tools.ietf.org/html/rfc3986). Beyond that it prevents using .NET's HTTP clients (HttpClient and WebClient) to talk to APIs that require accessing resources using escaped slashes unless you are using .NET 4.5.

# How can PUrify help?

PUrify will ensure that the Uri remains untouched. How does it do that? Well it hacks into the Uri internals and ensures that the original Uri is preserved.

# Hello PUrify
To use PUrify, create a Uri which you want to Purify. Then call the Purify extension method! It will do the rest! 

```csharp
static void Main(string[] args)
{
  var uri = new Uri("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
  Console.WriteLine("Before PUrify:");
  ShowUriDetails(uri);           
  uri.Purify();
  Console.WriteLine("After PUrify:");
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
```

Running this code will output the following:

```
Before PUrify:
        uri.ToString() - http://www.myapi.com//?Foo=Bar/#frag
        uri.AbsoluteUri - http://www.myapi.com//?Foo=Bar%2F#frag
        uri.Host - www.myapi.com
        uri.Query - ?Foo=Bar%2F
        uri.PathAndQuery - //?Foo=Bar%2F
        uri.AbsolutePath - //
        uri.Fragment - #frag
After PUrify:
        uri.ToString() - http://www.myapi.com/%2F?Foo=Bar%2F#frag
        uri.AbsoluteUri - http://www.myapi.com/%2F?Foo=Bar%2F#frag
        uri.Host - www.myapi.com
        uri.Query - ?Foo=Bar%2F
        uri.PathAndQuery - /%2F?Foo=Bar%2F
        uri.AbsolutePath - /%2F
        uri.Fragment - #frag

```

As you can see, the Uri has been PUrified!  Now you can take that Uri and use it with your favorite .NET Http client.

# Platforms

.NET 3.5, 4.0, and Mono > 1.x

# Acknowledgements
Credits to the following folks:

* [Rasmus Faber] (http://dk.linkedin.com/pub/rasmus-faber-espensen/5/92/880) for the .NET implementation of the workaround posted [here] (http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash)

* [Miguel De Icaza] (https://github.com/migueldeicaza) for his technical assistance on the Mono Uri implementation, and for offering to help ensure Mono will continue to be support this workaround.

* [Matt McClure] (https://github.com/matthewlmcclure) for his review of the Mono version and for pointing out how to properly handle fragments.


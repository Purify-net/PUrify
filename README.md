PUrify - Purify your Uri
======

# Why do I need my Uris purified?

The Uri classes in .NET prior to 4.5 and Mono scrub through your Uris and modify them in order to prevent vulnerabilities, for example escaped slashes are unescaped. This scrubbing however prevents Uris that are inline with [RFC 3986] (http://tools.ietf.org/html/rfc3986). Beyond that it prevents using .NET's HTTP clients (HttpClient and WebClient) to talk to APIs that require accessing resources using escaped slashes unless you are using .NET 4.5.

# How can PUrify help?

PUrify will ensure that the Uri remains untouched. How does it do that? Well it hacks into the Uri internals and ensures that the original Uri is preserved.

# Hello Purify
To use Purify simply instantiate a Purifier instance and call Purify! It will do the rest!

```csharp
    static void Main(string[] args)
    {
      var uri = new Uri("http://www.myapi.com/%2F?Foo=Bar%2F#frag");
      var purifier = new Purifier();
      purifier.Purify(uri);
      Console.WriteLine ("uri.ToString() - " + uri.ToString ());
      Console.WriteLine ("uri.AbsoluteUri - " + uri.AbsoluteUri);
      Console.WriteLine ("uri.Host - " + uri.Host);
      Console.WriteLine ("uri.Query - " + uri.Query);
      Console.WriteLine ("uri.PathAndQuery - " + uri.PathAndQuery);
      Console.WriteLine ("uri.AbsolutePath - " + uri.AbsolutePath);
      Console.WriteLine ("uri.Fragment - " + uri.Fragment);
    }
```

# Platforms

.NET 3.5, 4.0, and Mono > 1.x


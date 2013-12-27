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
  var uriString = "http://www.myapi.com/%2F?Foo=Bar%2F#frag";
  var uri = new Uri(uriString);
  Console.WriteLine("Uri String\n\t" + uriString);
  Console.WriteLine("\nBefore PUrify:");
  ShowUriDetails(uri);           
  uri.Purify();
  Console.WriteLine("\nAfter PUrify:");
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

## In .NET

![dotnet] (https://dl-web.dropbox.com/get/Public/Purify%20Windows.PNG?w=AACT-xT-8fOVgVbBjYK_ghYvsQDDl0r6GoK64g06FCpaGA)

## and in Mono

![mono] (https://dl-web.dropbox.com/get/Public/Purify%20Mono.png?w=AABcE0QR6Yo1mbK9Spo8Q5qS1f1Zk05ctUNcjWQev68oYg)

As you can see, the Uri has been PUrified!  Now you can take that Uri and use it with your favorite .NET Http client.

# Platforms

.NET 3.5, 4.0, and Mono > 1.x

# Acknowledgements
Credits to the following folks:

* [Rasmus Faber] (http://dk.linkedin.com/pub/rasmus-faber-espensen/5/92/880) for the .NET implementation of the workaround posted [here] (http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash)

* [Miguel De Icaza] (https://github.com/migueldeicaza) for his technical assistance on the Mono Uri implementation, and for offering to help ensure Mono will continue to be support this workaround.

* [Matt McClure] (https://github.com/matthewlmcclure) for his review of the Mono version and for pointing out how to properly handle fragments.


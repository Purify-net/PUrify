PUrify
======

# What is it?

PUrify will purify your Uri.

The Uri classes in .NET and Mono scrub through your Uris and modify them in order to prevent vulnerabilities, for example escaped slashes are unescaped. This scrubbing however prevents Uris that are inline with [RFC 3986] (http://tools.ietf.org/html/rfc3986). Beyond that it prevents using .NET's HTTP clients (HttpClient and WebClient) to talk to APIs that require accessing resources using escaped slashes.

PURify to the rescue.....

PUrify will ensure that the Uri remains untouched.

# Platforms

.NET 3.5, 4.0, and Mono > 1.x

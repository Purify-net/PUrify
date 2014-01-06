using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Purify
{
    internal class UriInfo
    {
        public string Path { get; private set; }
        public string Query { get; private set; }

        public UriInfo(Uri uri, string source)
        {
            var fragPos = source.IndexOf("#");
            var queryPos = source.IndexOf("?");
            var start = source.IndexOf(uri.Host) + uri.Host.Length;
            var pathEnd = queryPos == -1 ? fragPos : queryPos;
            if (pathEnd == -1)
                pathEnd = source.Length + 1;
            Path = queryPos > -1 ? source.Substring(start, pathEnd - start) : source.Substring(start);

            Query = fragPos > -1 ? source.Substring(queryPos, fragPos - queryPos) : null;

        }
    }
}

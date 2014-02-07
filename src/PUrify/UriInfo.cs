using System;
using System.Collections.Generic;
using System.Globalization;
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
            var fragPos = source.IndexOf("#", StringComparison.Ordinal);
            var queryPos = source.IndexOf("?", StringComparison.Ordinal);
            var start = source.IndexOf(uri.Host, StringComparison.Ordinal) + uri.Host.Length;
            var pathEnd = queryPos == -1 ? fragPos : queryPos;
            
            if (pathEnd == -1)
                pathEnd = source.Length + 1;

            if (start < pathEnd - 1 && source[start] == ':')
            {
                var portLength = uri.Port.ToString(CultureInfo.InvariantCulture).Length;
                start += portLength + 1;
            }

            Path = queryPos > -1 ? source.Substring(start, pathEnd - start) : source.Substring(start);

            Query = fragPos > -1 ? source.Substring(queryPos, fragPos - queryPos) : null;

        }
    }
}

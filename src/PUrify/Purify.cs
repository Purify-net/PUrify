using System;
using System.Reflection;

namespace Purify
{
	public static class Purifier
	{
		private static Type uriType = typeof(Uri);
		private static FieldInfo mono_sourceField;
		private static FieldInfo mono_queryField;
		private static FieldInfo mono_pathField;
		private static FieldInfo mono_cachedToStringField;
		private static FieldInfo mono_cachedAbsoluteUriField;
		private static FieldInfo flagsField;
		private static bool isMono;

		static Purifier()
		{
			flagsField = uriType.GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);

			//if m_flags does not exist we are in mono
			if (flagsField == null) {
				isMono = true;
				mono_sourceField = uriType.GetField ("source", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_queryField = uriType.GetField ("query", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_pathField = uriType.GetField ("path", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_cachedToStringField = uriType.GetField ("cachedToString", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_cachedAbsoluteUriField = uriType.GetField ("cachedAbsoluteUri", BindingFlags.NonPublic | BindingFlags.Instance);
			}
		}

		public static void Purify(this Uri uri)
		{
			if (isMono)
				PurifyMono (uri);
			else
				PurifyDotNet (uri);
		}

		private static void PurifyMono(Uri uri) {
			var source = (string) mono_sourceField.GetValue (uri);
			mono_cachedToStringField.SetValue (uri, source);
			mono_cachedAbsoluteUriField.SetValue (uri, source);
			var fragPos = source.IndexOf ("#");
			var queryPos = source.IndexOf ("?");
			var start = source.IndexOf (uri.Host) + uri.Host.Length;
			var pathEnd = queryPos == -1 ? fragPos : queryPos;
			if (pathEnd == -1)
				pathEnd = source.Length+1;
			var path = queryPos > -1 ? source.Substring (start, pathEnd - start) : source.Substring (start);
			mono_pathField.SetValue (uri, path);
			mono_queryField.SetValue(uri, fragPos > -1 ? source.Substring(queryPos, fragPos - queryPos) : source.Substring(queryPos));
		}

		private static void PurifyDotNet(Uri uri) {
			string paq = uri.PathAndQuery; // need to access PathAndQuery
			ulong flags = (ulong) flagsField.GetValue(uri);
			flags &= ~((ulong) 0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
			flagsField.SetValue(uri, flags);
		}
	}
}

